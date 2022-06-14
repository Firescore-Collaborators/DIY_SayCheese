// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "DissolveShader"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_BaseTex("BaseTex", 2D) = "white" {}
		_BaseColor("BaseColor", Color) = (1,1,1,0)
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_DissolveAmount("DissolveAmount", Range( 0 , 1)) = 1
		[Toggle(_DIRECTIONSWITCH_ON)] _DirectionSwitch("DirectionSwitch", Float) = 1
		[Toggle(_STARTSWITCH_VERTICAL_ON)] _StartSwitch_Vertical("StartSwitch_Vertical", Float) = 1
		[Toggle(_STARTSWITCH_HORIZONTAL_ON)] _StartSwitch_Horizontal("StartSwitch_Horizontal", Float) = 1
		[HideInInspector] _texcoord2( "", 2D ) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma shader_feature_local _DIRECTIONSWITCH_ON
		#pragma shader_feature_local _STARTSWITCH_HORIZONTAL_ON
		#pragma shader_feature_local _STARTSWITCH_VERTICAL_ON
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float2 uv2_texcoord2;
		};

		uniform sampler2D _BaseTex;
		uniform float4 _BaseTex_ST;
		uniform float4 _BaseColor;
		uniform float _Metallic;
		uniform float _Smoothness;
		uniform float _DissolveAmount;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_BaseTex = i.uv_texcoord * _BaseTex_ST.xy + _BaseTex_ST.zw;
			o.Albedo = ( tex2D( _BaseTex, uv_BaseTex ) * _BaseColor ).rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
			#ifdef _STARTSWITCH_HORIZONTAL_ON
				float staticSwitch18 = i.uv2_texcoord2.x;
			#else
				float staticSwitch18 = ( 1.0 - i.uv2_texcoord2.x );
			#endif
			#ifdef _STARTSWITCH_VERTICAL_ON
				float staticSwitch16 = i.uv2_texcoord2.y;
			#else
				float staticSwitch16 = ( 1.0 - i.uv2_texcoord2.y );
			#endif
			#ifdef _DIRECTIONSWITCH_ON
				float staticSwitch8 = staticSwitch16;
			#else
				float staticSwitch8 = staticSwitch18;
			#endif
			clip( step( staticSwitch8 , _DissolveAmount ) - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18935
0;0;1280;659;2727.995;-196.6495;2.03196;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-1839.066,804.1028;Inherit;False;1;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;19;-1531.924,776.8805;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;17;-1592.451,1016.265;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;16;-1337.451,1012.265;Inherit;False;Property;_StartSwitch_Vertical;StartSwitch_Vertical;7;0;Create;True;0;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;18;-1333.924,797.8805;Inherit;False;Property;_StartSwitch_Horizontal;StartSwitch_Horizontal;8;0;Create;True;0;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;7;-1125.977,15.07833;Inherit;False;Property;_BaseColor;BaseColor;2;0;Create;True;0;0;0;False;0;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-1133.839,-186.493;Inherit;True;Property;_BaseTex;BaseTex;1;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;4;-959.3291,1116.393;Inherit;False;Property;_DissolveAmount;DissolveAmount;5;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;8;-941.3749,818.5955;Inherit;False;Property;_DirectionSwitch;DirectionSwitch;6;0;Create;True;0;0;0;False;0;False;0;1;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-794.7874,-2.138845;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-477.6906,151.6342;Inherit;False;Property;_Smoothness;Smoothness;4;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-478.6907,67.6344;Inherit;False;Property;_Metallic;Metallic;3;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;3;-639.7957,862.9889;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;DissolveShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;False;Transparent;;Geometry;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;19;0;6;1
WireConnection;17;0;6;2
WireConnection;16;1;17;0
WireConnection;16;0;6;2
WireConnection;18;1;19;0
WireConnection;18;0;6;1
WireConnection;8;1;18;0
WireConnection;8;0;16;0
WireConnection;9;0;1;0
WireConnection;9;1;7;0
WireConnection;3;0;8;0
WireConnection;3;1;4;0
WireConnection;0;0;9;0
WireConnection;0;3;11;0
WireConnection;0;4;12;0
WireConnection;0;10;3;0
ASEEND*/
//CHKSM=7A31BECF0003E4DEA29AAC0EC7ECAC5854B2CE00