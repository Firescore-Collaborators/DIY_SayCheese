// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "TextureLerp"
{
	Properties
	{
		[Header(TextureA)][Space]_TextureA_BaseColor("TextureA_BaseColor", Color) = (1,1,1,0)
		_TextureA_BaseTex("TextureA_BaseTex", 2D) = "white" {}
		_TextureA_Normal("TextureA_Normal", 2D) = "bump" {}
		[Header(TextureB)][Space]_TextureB_BaseColor("TextureB_BaseColor", Color) = (1,1,1,0)
		_TextureB_BaseTex("TextureB_BaseTex", 2D) = "white" {}
		_TextureB_Normal("TextureB_Normal", 2D) = "bump" {}
		[Space]_NormalStrength("NormalStrength", Float) = 0
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Smothness("Smothness", Range( 0 , 1)) = 0
		_Transition("Transition", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TextureA_Normal;
		uniform float4 _TextureA_Normal_ST;
		uniform float _NormalStrength;
		uniform sampler2D _TextureB_Normal;
		uniform float4 _TextureB_Normal_ST;
		uniform float _Transition;
		uniform sampler2D _TextureA_BaseTex;
		uniform float4 _TextureA_BaseTex_ST;
		uniform float4 _TextureA_BaseColor;
		uniform sampler2D _TextureB_BaseTex;
		uniform float4 _TextureB_BaseTex_ST;
		uniform float4 _TextureB_BaseColor;
		uniform float _Metallic;
		uniform float _Smothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureA_Normal = i.uv_texcoord * _TextureA_Normal_ST.xy + _TextureA_Normal_ST.zw;
			float2 uv_TextureB_Normal = i.uv_texcoord * _TextureB_Normal_ST.xy + _TextureB_Normal_ST.zw;
			float3 lerpResult8 = lerp( UnpackScaleNormal( tex2D( _TextureA_Normal, uv_TextureA_Normal ), _NormalStrength ) , UnpackScaleNormal( tex2D( _TextureB_Normal, uv_TextureB_Normal ), _NormalStrength ) , _Transition);
			o.Normal = lerpResult8;
			float2 uv_TextureA_BaseTex = i.uv_texcoord * _TextureA_BaseTex_ST.xy + _TextureA_BaseTex_ST.zw;
			float2 uv_TextureB_BaseTex = i.uv_texcoord * _TextureB_BaseTex_ST.xy + _TextureB_BaseTex_ST.zw;
			float4 lerpResult3 = lerp( ( tex2D( _TextureA_BaseTex, uv_TextureA_BaseTex ) * _TextureA_BaseColor ) , ( tex2D( _TextureB_BaseTex, uv_TextureB_BaseTex ) * _TextureB_BaseColor ) , _Transition);
			o.Albedo = lerpResult3.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	//CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18935
0;0;1280;659;1874.877;-76.98354;1.538343;True;False
Node;AmplifyShaderEditor.ColorNode;9;-1046.974,-358.4552;Inherit;False;Property;_TextureA_BaseColor;TextureA_BaseColor;0;1;[Header];Create;True;1;TextureA;0;0;False;1;Space;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;10;-1047.881,47.27934;Inherit;False;Property;_TextureB_BaseColor;TextureB_BaseColor;3;1;[Header];Create;True;1;TextureB;0;0;False;1;Space;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-1049.393,-554.1049;Inherit;True;Property;_TextureA_BaseTex;TextureA_BaseTex;1;0;Create;True;1;TextureA;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-1050.064,-157.8944;Inherit;True;Property;_TextureB_BaseTex;TextureB_BaseTex;4;1;[Header];Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;7;-1147.032,609.9267;Inherit;False;Property;_NormalStrength;NormalStrength;6;0;Create;True;0;0;0;False;1;Space;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-826.6213,317.6715;Inherit;False;Property;_Transition;Transition;9;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-724.5529,26.83623;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-664.7733,-378.48;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;5;-839.2475,475.39;Inherit;True;Property;_TextureA_Normal;TextureA_Normal;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;6;-837.0551,685.2292;Inherit;True;Property;_TextureB_Normal;TextureB_Normal;5;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;3;-503.2254,-1.126189;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;8;-411.5733,481.4948;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-357.896,96.42075;Inherit;False;Property;_Metallic;Metallic;7;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-356.8827,178.4834;Inherit;False;Property;_Smothness;Smothness;8;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;91.89337,-2.265868;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;TextureLerp;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;12;0;2;0
WireConnection;12;1;10;0
WireConnection;11;0;1;0
WireConnection;11;1;9;0
WireConnection;5;5;7;0
WireConnection;6;5;7;0
WireConnection;3;0;11;0
WireConnection;3;1;12;0
WireConnection;3;2;4;0
WireConnection;8;0;5;0
WireConnection;8;1;6;0
WireConnection;8;2;4;0
WireConnection;0;0;3;0
WireConnection;0;1;8;0
WireConnection;0;3;13;0
WireConnection;0;4;14;0
ASEEND*/
//CHKSM=8711CC5B8F910118635B9A2E19A4CE8A73458FFD