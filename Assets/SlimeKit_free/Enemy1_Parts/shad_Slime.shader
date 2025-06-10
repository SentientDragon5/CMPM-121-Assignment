// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:Unlit/Texture,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2865,x:32956,y:32647,varname:node_2865,prsc:2|normal-6857-RGB,emission-5958-RGB,custl-9522-OUT,alpha-1081-OUT,refract-5447-OUT;n:type:ShaderForge.SFN_Tex2d,id:7736,x:33020,y:33485,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:_Diffuse,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:358,x:32135,y:32753,ptovrint:False,ptlb:Transparency,ptin:_Transparency,varname:_Transparency,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5470086,max:1;n:type:ShaderForge.SFN_Slider,id:1813,x:32479,y:34290,ptovrint:False,ptlb:Spec Intense,ptin:_SpecIntense,varname:_SpecIntense,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:7.726496,max:8;n:type:ShaderForge.SFN_Fresnel,id:6943,x:32767,y:31948,varname:node_6943,prsc:2|EXP-4829-OUT;n:type:ShaderForge.SFN_Slider,id:4829,x:32497,y:31867,ptovrint:False,ptlb:Edge Glow Amount,ptin:_EdgeGlowAmount,varname:_EdgeGlowAmount,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.666667,max:6;n:type:ShaderForge.SFN_Color,id:5823,x:32383,y:31916,ptovrint:False,ptlb:Edge Glow Colour,ptin:_EdgeGlowColour,varname:_EdgeGlowColour,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.022,c2:0.5230861,c3:0.5230861,c4:0.178;n:type:ShaderForge.SFN_Multiply,id:9918,x:32795,y:32112,varname:node_9918,prsc:2|A-6943-OUT,B-5823-RGB;n:type:ShaderForge.SFN_Add,id:4791,x:32941,y:33174,varname:node_4791,prsc:2|A-9918-OUT,B-675-OUT;n:type:ShaderForge.SFN_ViewReflectionVector,id:3462,x:31819,y:33969,varname:node_3462,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6046,x:32922,y:33917,varname:node_6046,prsc:2|A-2257-OUT,B-1813-OUT;n:type:ShaderForge.SFN_LightVector,id:4229,x:31713,y:33805,varname:node_4229,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:1394,x:31864,y:33727,prsc:2,pt:True;n:type:ShaderForge.SFN_Dot,id:381,x:32056,y:33656,varname:node_381,prsc:2,dt:0|A-4229-OUT,B-1394-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5442,x:32417,y:33919,ptovrint:False,ptlb:(0.5),ptin:_05,varname:_05,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Add,id:6103,x:32492,y:33662,varname:node_6103,prsc:2|A-2988-OUT,B-5442-OUT;n:type:ShaderForge.SFN_Multiply,id:1655,x:32614,y:33533,varname:node_1655,prsc:2|A-6103-OUT,B-5442-OUT;n:type:ShaderForge.SFN_Multiply,id:2708,x:32789,y:33513,varname:node_2708,prsc:2|A-1655-OUT,B-1655-OUT;n:type:ShaderForge.SFN_Multiply,id:2988,x:32292,y:33694,varname:node_2988,prsc:2|A-381-OUT,B-9438-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:9438,x:32014,y:33528,varname:node_9438,prsc:2;n:type:ShaderForge.SFN_LightColor,id:7412,x:33224,y:32868,varname:node_7412,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9522,x:33426,y:33089,varname:node_9522,prsc:2|A-7509-OUT,B-7412-RGB;n:type:ShaderForge.SFN_Dot,id:4072,x:32018,y:33969,varname:node_4072,prsc:2,dt:1|A-4229-OUT,B-3462-OUT;n:type:ShaderForge.SFN_Power,id:2257,x:32234,y:33919,varname:node_2257,prsc:2|VAL-4072-OUT,EXP-2340-OUT;n:type:ShaderForge.SFN_Exp,id:2340,x:32283,y:34057,varname:node_2340,prsc:2,et:1|IN-8643-OUT;n:type:ShaderForge.SFN_Slider,id:8643,x:32479,y:34205,ptovrint:False,ptlb:Spec Amount,ptin:_SpecAmount,varname:_SpecAmount,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:8.743589,max:10;n:type:ShaderForge.SFN_Add,id:7509,x:33402,y:33227,varname:node_7509,prsc:2|A-4791-OUT,B-6046-OUT;n:type:ShaderForge.SFN_Tex2d,id:6857,x:32859,y:32311,ptovrint:False,ptlb:Normals,ptin:_Normals,varname:_Normals,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:cb6c5165ed180c543be39ed70e72abc8,ntxv:3,isnm:True;n:type:ShaderForge.SFN_NormalVector,id:4455,x:31298,y:32835,prsc:2,pt:False;n:type:ShaderForge.SFN_Transform,id:9644,x:31322,y:33039,varname:node_9644,prsc:2,tffrom:0,tfto:3|IN-4455-OUT;n:type:ShaderForge.SFN_ComponentMask,id:414,x:31445,y:33039,varname:node_414,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-9644-XYZ;n:type:ShaderForge.SFN_Multiply,id:5447,x:31657,y:32835,varname:node_5447,prsc:2|A-7636-OUT,B-8512-OUT;n:type:ShaderForge.SFN_Slider,id:8512,x:31657,y:33037,ptovrint:False,ptlb:Refract,ptin:_Refract,varname:_Refract,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.6837607,max:1;n:type:ShaderForge.SFN_ComponentMask,id:8097,x:32643,y:32410,varname:node_8097,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-6857-RGB;n:type:ShaderForge.SFN_Add,id:7636,x:31477,y:32835,varname:node_7636,prsc:2|A-8097-OUT,B-414-OUT;n:type:ShaderForge.SFN_Color,id:6799,x:33198,y:33696,ptovrint:False,ptlb:Dif Tint,ptin:_DifTint,varname:_DifTint,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:675,x:33404,y:33492,varname:node_675,prsc:2|A-7736-R,B-6799-RGB;n:type:ShaderForge.SFN_Tex2d,id:5958,x:32197,y:32493,ptovrint:False,ptlb:Emis Map,ptin:_EmisMap,varname:_EmisMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False|UVIN-2006-UVOUT;n:type:ShaderForge.SFN_Panner,id:2006,x:32038,y:32292,varname:node_2006,prsc:2,spu:1,spv:0|UVIN-6092-UVOUT,DIST-4223-TSL;n:type:ShaderForge.SFN_TexCoord,id:6092,x:31946,y:32117,varname:node_6092,prsc:2,uv:0;n:type:ShaderForge.SFN_Time,id:4223,x:31806,y:32341,varname:node_4223,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1081,x:32480,y:32734,varname:node_1081,prsc:2|A-5958-A,B-358-OUT;proporder:6857-358-8512-4829-5823-1813-8643-5442-6799-7736-5958;pass:END;sub:END;*/

Shader "Charlie/shad_Goop_v7" {
    Properties {
        _Normals ("Normals", 2D) = "bump" {}
        _Transparency ("Transparency", Range(0, 1)) = 0.5470086
        _Refract ("Refract", Range(-1, 1)) = 0.6837607
        _EdgeGlowAmount ("Edge Glow Amount", Range(0, 6)) = 2.666667
        _EdgeGlowColour ("Edge Glow Colour", Color) = (0.022,0.5230861,0.5230861,0.178)
        _SpecIntense ("Spec Intense", Range(0, 8)) = 7.726496
        _SpecAmount ("Spec Amount", Range(0, 10)) = 8.743589
        _05 ("(0.5)", Float ) = 0.5
        _DifTint ("Dif Tint", Color) = (0.5,0.5,0.5,1)
        _Diffuse ("Diffuse", 2D) = "white" {}
        _EmisMap ("Emis Map", 2D) = "black" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers d3d11 gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform float4 _TimeEditor;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float _Transparency;
            uniform float _SpecIntense;
            uniform float _EdgeGlowAmount;
            uniform float4 _EdgeGlowColour;
            uniform float _SpecAmount;
            uniform sampler2D _Normals; uniform float4 _Normals_ST;
            uniform float _Refract;
            uniform float4 _DifTint;
            uniform sampler2D _EmisMap; uniform float4 _EmisMap_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normals_var = UnpackNormal(tex2D(_Normals,TRANSFORM_TEX(i.uv0, _Normals)));
                float3 normalLocal = _Normals_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + ((_Normals_var.rgb.rg+mul( UNITY_MATRIX_V, float4(i.normalDir,0) ).xyz.rgb.rg)*_Refract);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
////// Emissive:
                float4 node_4223 = _Time + _TimeEditor;
                float2 node_2006 = (i.uv0+node_4223.r*float2(1,0));
                float4 _EmisMap_var = tex2D(_EmisMap,TRANSFORM_TEX(node_2006, _EmisMap));
                float3 emissive = _EmisMap_var.rgb;
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 finalColor = emissive + ((((pow(1.0-max(0,dot(normalDirection, viewDirection)),_EdgeGlowAmount)*_EdgeGlowColour.rgb)+(_Diffuse_var.r*_DifTint.rgb))+(pow(max(0,dot(lightDirection,viewReflectDirection)),exp2(_SpecAmount))*_SpecIntense))*_LightColor0.rgb);
                return fixed4(lerp(sceneColor.rgb, finalColor,(_EmisMap_var.a*_Transparency)),1);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd
            #pragma exclude_renderers d3d11 gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform float4 _TimeEditor;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float _Transparency;
            uniform float _SpecIntense;
            uniform float _EdgeGlowAmount;
            uniform float4 _EdgeGlowColour;
            uniform float _SpecAmount;
            uniform sampler2D _Normals; uniform float4 _Normals_ST;
            uniform float _Refract;
            uniform float4 _DifTint;
            uniform sampler2D _EmisMap; uniform float4 _EmisMap_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
                LIGHTING_COORDS(6,7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normals_var = UnpackNormal(tex2D(_Normals,TRANSFORM_TEX(i.uv0, _Normals)));
                float3 normalLocal = _Normals_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + ((_Normals_var.rgb.rg+mul( UNITY_MATRIX_V, float4(i.normalDir,0) ).xyz.rgb.rg)*_Refract);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 finalColor = ((((pow(1.0-max(0,dot(normalDirection, viewDirection)),_EdgeGlowAmount)*_EdgeGlowColour.rgb)+(_Diffuse_var.r*_DifTint.rgb))+(pow(max(0,dot(lightDirection,viewReflectDirection)),exp2(_SpecAmount))*_SpecIntense))*_LightColor0.rgb);
                float4 node_4223 = _Time + _TimeEditor;
                float2 node_2006 = (i.uv0+node_4223.r*float2(1,0));
                float4 _EmisMap_var = tex2D(_EmisMap,TRANSFORM_TEX(node_2006, _EmisMap));
                return fixed4(finalColor * (_EmisMap_var.a*_Transparency),0);
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers d3d11 gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _EmisMap; uniform float4 _EmisMap_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 node_4223 = _Time + _TimeEditor;
                float2 node_2006 = (i.uv0+node_4223.r*float2(1,0));
                float4 _EmisMap_var = tex2D(_EmisMap,TRANSFORM_TEX(node_2006, _EmisMap));
                o.Emission = _EmisMap_var.rgb;
                
                float3 diffColor = float3(0,0,0);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Unlit/Texture"
    CustomEditor "ShaderForgeMaterialInspector"
}
