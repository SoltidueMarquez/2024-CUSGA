Shader "UI/CircleMaskURP"
{
    Properties
    {
        _BaseMap ("Base Texture",2D) = "white"{}
        _BaseColor("Base Color",Color)=(1,1,1,1)
        _PositionX("PositionX",Range(0,1))=0.5
        _PositionY("PositionY",Range(0,1))=0.5
        _Radius("Radius",Range(0,2))=0.5
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"//这是一个URP Shader！
            "Queue"="Geometry"
            "RenderType"="Opaque"
        }
        HLSLINCLUDE
         //CG中核心代码库 #include "UnityCG.cginc"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
       
        //除了贴图外，要暴露在Inspector面板上的变量都需要缓存到CBUFFER中
        CBUFFER_START(UnityPerMaterial)
        float4 _BaseMap_ST;
        half4 _BaseColor;
        float _PositionX;
        float _PositionY;
        float _Radius;
        CBUFFER_END
        ENDHLSL

        Pass
        {
            Tags{"LightMode"="UniversalForward"}//这个Pass最终会输出到颜色缓冲里

            HLSLPROGRAM //CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct Attributes//这就是a2v
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD;
            };
            struct Varings//这就是v2f
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD;
            };

            TEXTURE2D(_BaseMap);//在CG中会写成sampler2D _MainTex;
            SAMPLER(sampler_BaseMap);
            
            
            Varings vert(Attributes IN)
            {
                Varings OUT;
                //在CG里面，我们这样转换空间坐标 o.vertex = UnityObjectToClipPos(v.vertex);
                VertexPositionInputs positionInputs = GetVertexPositionInputs(IN.positionOS.xyz);
                OUT.positionCS = positionInputs.positionCS;

                OUT.uv=TRANSFORM_TEX(IN.uv,_BaseMap);
                return OUT;
            }

            float4 frag(Varings IN):SV_Target
            {
                //在CG里，我们这样对贴图采样 fixed4 col = tex2D(_MainTex, i.uv);
                half4 baseMap = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, IN.uv); 
                //计算当前像素点到圆心的距离
                float aspectRatio = _ScreenParams.x / _ScreenParams.y;
                float2 center = float2(_PositionX * aspectRatio,_PositionY);
                float2 uv = IN.uv;
                uv.x *= aspectRatio;
                float radius = _Radius;
                float dis = distance(uv,center);
//如果          当前像素点到圆心的距离小于于半径，那么就让这个像素点变透明
				if(dis<radius)
				{
					discard;
				}
                return baseMap * _BaseColor;
            }
            ENDHLSL  //ENDCG          
        }
    }
}