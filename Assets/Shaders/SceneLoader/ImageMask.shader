Shader "UI/CircleMaskURP"
{
    Properties
    {
        _BaseMap ("Base Texture",2D) = "white"{}
        _BaseColor("Base Color",Color)=(1,1,1,1)
        _PositionX("PositionX",Range(0,1))=0.5
        _PositionY("PositionY",Range(0,1))=0.5
        _Radius("Radius",Range(0,1))=0.5
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"//����һ��URP Shader��
            "Queue"="Geometry"
            "RenderType"="Opaque"
        }
        HLSLINCLUDE
         //CG�к��Ĵ���� #include "UnityCG.cginc"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
       
        //������ͼ�⣬Ҫ��¶��Inspector����ϵı�������Ҫ���浽CBUFFER��
        CBUFFER_START(UnityPerMaterial)
        float4 _BaseMap_ST;
        half4 _BaseColor;
        CBUFFER_END
        ENDHLSL

        Pass
        {
            Tags{"LightMode"="UniversalForward"}//���Pass���ջ��������ɫ������

            HLSLPROGRAM //CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct Attributes//�����a2v
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD;
            };
            struct Varings//�����v2f
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD;
            };

            TEXTURE2D(_BaseMap);//��CG�л�д��sampler2D _MainTex;
            SAMPLER(sampler_BaseMap);
            float _PositionX;
            float _PositionY;
            float _Radius;
            
            Varings vert(Attributes IN)
            {
                Varings OUT;
                //��CG���棬��������ת���ռ����� o.vertex = UnityObjectToClipPos(v.vertex);
                VertexPositionInputs positionInputs = GetVertexPositionInputs(IN.positionOS.xyz);
                OUT.positionCS = positionInputs.positionCS;

                OUT.uv=TRANSFORM_TEX(IN.uv,_BaseMap);
                return OUT;
            }

            float4 frag(Varings IN):SV_Target
            {
                //��CG�������������ͼ���� fixed4 col = tex2D(_MainTex, i.uv);
                half4 baseMap = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, IN.uv); 
                //���㵱ǰ���ص㵽Բ�ĵľ���
                float aspectRatio = _ScreenParams.x / _ScreenParams.y;
                float2 center = float2(_PositionX * aspectRatio,_PositionY);
                float2 uv = IN.uv;
                uv.x *= aspectRatio;
                float radius = _Radius;
                float dis = distance(uv,center);
//���          ��ǰ���ص㵽Բ�ĵľ���С���ڰ뾶����ô����������ص��͸��
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