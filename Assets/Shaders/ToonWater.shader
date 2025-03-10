Shader "Custom/ToonWater" {
    Properties {
        _Shades ("Color Bands", Range(2, 5)) = 3
        _ShallowColor ("Shallow Color", Color) = (0.4, 0.8, 1, 1)
        _DeepColor ("Deep Color", Color) = (0.1, 0.3, 0.6, 1)
        _WaveSpeed ("Wave Speed", Float) = 0.3  // Reduced speed
        _WaveScale ("Wave Scale", Float) = 0.1
        _VoronoiScale ("Cell Size", Range(0.1, 20)) = 8.0
        _VoronoiIntensity ("Edge Strength", Range(0, 1)) = 0.4  // Reduced intensity
        _FoamWidth ("Foam Width", Range(0, 1)) = 0.25  // Narrower foam
    }

    SubShader {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
                float2 voronoiUV : TEXCOORD2;
            };

            // Properties
            float _Shades, _WaveSpeed, _WaveScale, _VoronoiScale, _VoronoiIntensity, _FoamWidth;
            float4 _ShallowColor, _DeepColor;
            sampler2D _CameraDepthTexture;

            // ========================================
            // Stabilized Voronoi Functions
            // ========================================
            float2 random2(float2 st) {
                st = float2(
                    dot(st, float2(127.1, 311.7)),
                    dot(st, float2(269.5, 183.3))
                );
                return frac(sin(st) * 43758.5453);
            }

            float3 voronoi(float2 uv) {
                uv *= _VoronoiScale;
                float2 i_st = floor(uv);
                float2 f_st = frac(uv);

                float min_dist = 1.0;
                float second_min_dist = 1.0;

                for (int y = -1; y <= 1; y++) {
                    for (int x = -1; x <= 1; x++) {
                        float2 neighbor = float2(x, y);
                        float2 p = random2(i_st + neighbor);
                        float2 diff = neighbor + p - f_st;
                        float dist = length(diff);
                        
                        if (dist < min_dist) {
                            second_min_dist = min_dist;
                            min_dist = dist;
                        }
                    }
                }
                return float3(min_dist, second_min_dist, 0);
            }

            float getVoronoiEdge(float2 uv) {
                float3 vor = voronoi(uv);
                float edge = saturate(vor.y - vor.x);  // Clamped edge calculation
                return smoothstep(0.0, 0.2, edge * 2.0);
            }
            // ========================================

            v2f vert(appdata v) {
                v2f o;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.voronoiUV = o.worldPos.xz;

                // Gentle wave motion
                float wave = sin(_Time.y * _WaveSpeed + o.worldPos.x * 2) * _WaveScale;
                v.vertex.y += wave * 0.2;  // Reduced displacement

                o.pos = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.pos);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                // Depth-based color (stable)
                float depth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos)));
                float surfaceDepth = saturate(1 - (depth - i.screenPos.w) / 5);
                float4 waterColor = lerp(_ShallowColor, _DeepColor, surfaceDepth);

                // Toon color bands (clamped)
                waterColor.rgb = floor(saturate(waterColor.rgb) * _Shades) / _Shades;

                // Voronoi pattern (stable edges)
                float edgePattern = getVoronoiEdge(i.voronoiUV * 0.8);
                waterColor.rgb *= 1.0 - edgePattern * _VoronoiIntensity;

                // Smooth foam with clamped animation
                float foamDepth = smoothstep(0.7, 0.9, surfaceDepth);
                float foamWave = 0.5 + 0.5 * sin(_Time.y * 1.5);  // Slower oscillation
                float foam = saturate(foamDepth * edgePattern * foamWave) * 0.4;
                waterColor.rgb += foam;

                // Final color clamping
                waterColor.rgb = saturate(waterColor.rgb);
                waterColor.a = _ShallowColor.a * (0.7 + surfaceDepth * 0.3);  // Stable alpha

                return waterColor;
            }
            ENDCG
        }
    }
}
