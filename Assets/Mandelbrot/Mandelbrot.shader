Shader "Explorer/Mandelbrot"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Area("Area", vector) = (0,0,4,4)
		_Angle("Angle", range(-3.1415, 3.1415)) = 0
		_Color("Color", range(0,1)) = .5
		_Repeat("Repeat", float) = 1
		_Speed("Speed", range(-1,1)) = 0.1
		_Speed2("Speed2", range(-5,5)) = 1
		_Symmetry("Symmetry", range(0,1)) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
			float4 _Area;
			float _Angle;
            sampler2D _MainTex;
			float _Color, _Repeat, _Speed, _Speed2, _Symmetry;

			float2 rot(float2 p, float2 pivot, float angle){
				float sinA = sin(angle);
				float cosA = cos(angle);
				p -= pivot;
				p = float2(p.x * cosA - p.y*sinA, p.x*sinA + p.y*cosA);
				p+=pivot;

				return p;
			}
            fixed4 frag (v2f i) : SV_Target
            {
				float2 uv = i.uv - 0.5;
				uv = abs(uv);
				uv = rot(uv, 0, .25*3.1415);
				uv = abs(uv);
				uv = lerp(i.uv - .5, uv, _Symmetry);
				//uv = uv*uv;
                float2 c = _Area.xy + uv*_Area.zw; 
				c = rot(c, _Area.xy, _Angle);
				float r = 20;
				float r2 = r*r;
				float2 z, zPrev;
				float iter;
				
				for(iter = 0; iter < 255; iter++)
				{
					zPrev = rot(z, 0, _Time.y* _Speed2);
					z = float2(z.x*z.x-z.y*z.y, 2*z.x*z.y) + c;
					if(dot(z,zPrev) > r2)
					//if(dot(z,z) > r2)
					{
						break;
					}
				}
				if(iter == 255) 
				{
					return 0;
				}
				float dist = length(z);
				float fracIter = (dist-r)/(r2-r);
				fracIter = log2(log(dist)/log(r));
				iter -= fracIter;
				float b = sqrt(iter/255);
				float4 color = sin(float4(.3, .45, .65, 1)*b*10)*.5+.5;
                color = tex2D(_MainTex, float2(b*_Repeat * _Time.y *_Speed, _Color));
				color *= smoothstep(3,0, fracIter);
				float angle = atan2(z.x, z.y);
				color *= 1 + sin(angle *2 + _Time.y * 4 * _Speed2) * .2;
				return color;
            }
            ENDCG
        }
    }
}
