// Crest Ocean System

// This file is subject to the MIT License as seen in the root of this folder structure (LICENSE)

Shader "Crest/Underwater/Ocean Mask"
{
	SubShader
	{
		Pass
		{
			// We always disable culling when rendering ocean mask, as we only
			// use it for underwater rendering features.
			Cull Off

			HLSLPROGRAM
			#pragma vertex Vert
			#pragma fragment Frag
			// for VFACE
			#pragma target 3.0

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"

			struct Attributes
			{
				// The old unity macros require this name and type.
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct Varyings
			{
				float4 positionCS : SV_POSITION;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			#include "../OceanConstants.hlsl"
			#include "../OceanInputsDriven.hlsl"
			#include "../OceanGlobals.hlsl"
			#include "../OceanHelpersNew.hlsl"
			#include "../OceanVertHelpers.hlsl"

			// Hack - due to SV_IsFrontFace occasionally coming through as true for backfaces,
			// add a param here that forces ocean to be in undrwater state. I think the root
			// cause here might be imprecision or numerical issues at ocean tile boundaries, although
			// i'm not sure why cracks are not visible in this case.
			float _ForceUnderwater;

			Varyings Vert(Attributes v)
			{
				Varyings output;

				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

				const CascadeParams cascadeData0 = _CrestCascadeData[_LD_SliceIndex];
				const CascadeParams cascadeData1 = _CrestCascadeData[_LD_SliceIndex + 1];
				const PerCascadeInstanceData instanceData = _CrestPerCascadeInstanceData[_LD_SliceIndex];

				float3 worldPos = mul(UNITY_MATRIX_M, float4(v.vertex.xyz, 1.0));

#if (SHADEROPTIONS_CAMERA_RELATIVE_RENDERING != 0)
				worldPos.xz += _WorldSpaceCameraPos.xz;
#endif

				// Vertex snapping and lod transition
				float lodAlpha;
				const float meshScaleLerp = instanceData._meshScaleLerp;
				const float gridSize = instanceData._geoGridWidth;
				SnapAndTransitionVertLayout(meshScaleLerp, cascadeData0, gridSize, worldPos, lodAlpha);

				// Scale up by small "epsilon" to solve numerical issues. Expand slightly about tile center.
				// :OceanGridPrecisionErrors
				float2 tileCenterXZ = UNITY_MATRIX_M._m03_m23;
#if (SHADEROPTIONS_CAMERA_RELATIVE_RENDERING != 0)
				tileCenterXZ += _WorldSpaceCameraPos.xz;
#endif
				worldPos.xz = lerp( tileCenterXZ, worldPos.xz, 1.0001 );

				// Calculate sample weights. params.z allows shape to be faded out (used on last lod to support pop-less scale transitions)
				const float wt_smallerLod = (1. - lodAlpha) * cascadeData0._weight;
				const float wt_biggerLod = (1. - wt_smallerLod) * cascadeData1._weight;
				// Sample displacement textures, add results to current world pos / normal / foam
				const float2 positionWS_XZ_before = worldPos.xz;

				// Data that needs to be sampled at the undisplaced position
				if (wt_smallerLod > 0.001)
				{
					half sss = 0.0;
					const float3 uv_slice_smallerLod = WorldToUV(positionWS_XZ_before, cascadeData0, _LD_SliceIndex);
					SampleDisplacements(_LD_TexArray_AnimatedWaves, uv_slice_smallerLod, wt_smallerLod, worldPos, sss);
				}
				if (wt_biggerLod > 0.001)
				{
					half sss = 0.0;
					const float3 uv_slice_biggerLod = WorldToUV(positionWS_XZ_before, cascadeData1, _LD_SliceIndex + 1);
					SampleDisplacements(_LD_TexArray_AnimatedWaves, uv_slice_biggerLod, wt_biggerLod, worldPos, sss);
				}

#if (SHADEROPTIONS_CAMERA_RELATIVE_RENDERING != 0)
				worldPos.xz -= _WorldSpaceCameraPos.xz;
#endif

				output.positionCS = mul(UNITY_MATRIX_VP, float4(worldPos, 1.0));
				return output;
			}

			half4 Frag(const Varyings input, const float facing : VFACE) : SV_Target
			{
				if (IsUnderwater(facing, _ForceUnderwater))
				{
					return (half4)UNDERWATER_MASK_WATER_SURFACE_BELOW;
				}
				else
				{
					return (half4)UNDERWATER_MASK_WATER_SURFACE_ABOVE;
				}
			}
			ENDHLSL
		}
	}
}
