// Compiled shader for PC, Mac & Linux Standalone

//////////////////////////////////////////////////////////////////////////
// 
// NOTE: This is *not* a valid shader file, the contents are provided just
// for information and for debugging purposes only.
// 
//////////////////////////////////////////////////////////////////////////
// Skipping shader variants that would not be included into build of current scene.

Shader "Black Hole" {
Properties {
 _MainTex ("Main", 2D) = "white" { }
 _Center ("Center", Vector) = (0.500000,0.500000,0.000000,0.000000)
 _Distortion ("Distortion", Float) = -2.000000
 _DarkRange ("Dark Range", Float) = 0.100000
}
SubShader { 
 Pass {
  ZTest False
  ZWrite Off
  Cull Off
  //////////////////////////////////
  //                              //
  //      Compiled programs       //
  //                              //
  //////////////////////////////////
//////////////////////////////////////////////////////
No keywords set in this variant.
-- Hardware tier variant: Tier 1
-- Vertex shader for "metal":
Uses vertex data channel "Vertex"
Uses vertex data channel "TexCoord"

Constant Buffer "VGlobals" (128 bytes) on slot 0 {
  Matrix4x4 unity_ObjectToWorld at 0
  Matrix4x4 unity_MatrixVP at 64
}

Shader Disassembly:
#include <metal_stdlib>
#include <metal_texture>
using namespace metal;

#if !(__HAVE_FMA__)
#define fma(a,b,c) ((a) * (b) + (c))
#endif

struct VGlobals_Type
{
    float4 hlslcc_mtx4x4unity_ObjectToWorld[4];
    float4 hlslcc_mtx4x4unity_MatrixVP[4];
};

struct Mtl_VertexIn
{
    float4 POSITION0 [[ attribute(0) ]] ;
    float2 TEXCOORD0 [[ attribute(1) ]] ;
};

struct Mtl_VertexOut
{
    float4 mtl_Position [[ position ]];
    float2 TEXCOORD0 [[ user(TEXCOORD0) ]];
};

vertex Mtl_VertexOut xlatMtlMain(
    constant VGlobals_Type& VGlobals [[ buffer(0) ]],
    Mtl_VertexIn input [[ stage_in ]])
{
    Mtl_VertexOut output;
    float4 u_xlat0;
    float4 u_xlat1;
    u_xlat0 = input.POSITION0.yyyy * VGlobals.hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat0 = fma(VGlobals.hlslcc_mtx4x4unity_ObjectToWorld[0], input.POSITION0.xxxx, u_xlat0);
    u_xlat0 = fma(VGlobals.hlslcc_mtx4x4unity_ObjectToWorld[2], input.POSITION0.zzzz, u_xlat0);
    u_xlat0 = u_xlat0 + VGlobals.hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat1 = u_xlat0.yyyy * VGlobals.hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat1 = fma(VGlobals.hlslcc_mtx4x4unity_MatrixVP[0], u_xlat0.xxxx, u_xlat1);
    u_xlat1 = fma(VGlobals.hlslcc_mtx4x4unity_MatrixVP[2], u_xlat0.zzzz, u_xlat1);
    output.mtl_Position = fma(VGlobals.hlslcc_mtx4x4unity_MatrixVP[3], u_xlat0.wwww, u_xlat1);
    output.TEXCOORD0.xy = input.TEXCOORD0.xy;
    return output;
}


-- Hardware tier variant: Tier 1
-- Fragment shader for "metal":
Set 2D Texture "_MainTex" to slot 0

Constant Buffer "FGlobals" (32 bytes) on slot 0 {
  Vector4 _ScreenParams at 0
  Vector2 _Center at 16
  Float _Distortion at 24
  Float _DarkRange at 28
}

Shader Disassembly:
#include <metal_stdlib>
#include <metal_texture>
using namespace metal;

#if !(__HAVE_FMA__)
#define fma(a,b,c) ((a) * (b) + (c))
#endif

#ifndef XLT_REMAP_O
	#define XLT_REMAP_O {0, 1, 2, 3, 4, 5, 6, 7}
#endif
constexpr constant uint xlt_remap_o[] = XLT_REMAP_O;
struct FGlobals_Type
{
    float4 _ScreenParams;
    float2 _Center;
    float _Distortion;
    float _DarkRange;
};

struct Mtl_FragmentIn
{
    float2 TEXCOORD0 [[ user(TEXCOORD0) ]] ;
};

struct Mtl_FragmentOut
{
    float4 SV_TARGET0 [[ color(xlt_remap_o[0]) ]];
};

fragment Mtl_FragmentOut xlatMtlMain(
    constant FGlobals_Type& FGlobals [[ buffer(0) ]],
    sampler sampler_MainTex [[ sampler (0) ]],
    texture2d<float, access::sample > _MainTex [[ texture(0) ]] ,
    Mtl_FragmentIn input [[ stage_in ]])
{
    Mtl_FragmentOut output;
    float2 u_xlat0;
    float4 u_xlat1;
    float2 u_xlat4;
    float u_xlat6;
    u_xlat0.xy = (-input.TEXCOORD0.xy) + FGlobals._Center.xyxx.xy;
    u_xlat4.x = dot(u_xlat0.xy, u_xlat0.xy);
    u_xlat4.x = rsqrt(u_xlat4.x);
    u_xlat0.xy = u_xlat4.xx * u_xlat0.xy;
    u_xlat4.xy = input.TEXCOORD0.xy * FGlobals._ScreenParams.xy;
    u_xlat4.xy = fma(FGlobals._Center.xyxx.xy, FGlobals._ScreenParams.xy, (-u_xlat4.xy));
    u_xlat4.x = dot(u_xlat4.xy, u_xlat4.xy);
    u_xlat4.x = sqrt(u_xlat4.x);
    u_xlat6 = log2(u_xlat4.x);
    u_xlat4.x = fma(FGlobals._DarkRange, u_xlat4.x, -1.5);
    u_xlat4.x = clamp(u_xlat4.x, 0.0f, 1.0f);
    u_xlat6 = u_xlat6 * FGlobals._Distortion;
    u_xlat6 = exp2(u_xlat6);
    u_xlat0.xy = float2(u_xlat6) * u_xlat0.xy;
    u_xlat1.xy = u_xlat0.xy * float2(30.0, 30.0);
    u_xlat1.z = (-u_xlat1.y);
    u_xlat0.xy = u_xlat1.xz + input.TEXCOORD0.xy;
    u_xlat1 = _MainTex.sample(sampler_MainTex, u_xlat0.xy);
    output.SV_TARGET0 = u_xlat4.xxxx * u_xlat1;
    return output;
}


//////////////////////////////////////////////////////
No keywords set in this variant.
-- Hardware tier variant: Tier 1
-- Vertex shader for "glcore":
Shader Disassembly:
#ifdef VERTEX
#version 150
#extension GL_ARB_explicit_attrib_location : require
#ifdef GL_ARB_shader_bit_encoding
#extension GL_ARB_shader_bit_encoding : enable
#endif

uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
in  vec4 in_POSITION0;
in  vec2 in_TEXCOORD0;
out vec2 vs_TEXCOORD0;
vec4 u_xlat0;
vec4 u_xlat1;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
    gl_Position = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
    return;
}

#endif
#ifdef FRAGMENT
#version 150
#extension GL_ARB_explicit_attrib_location : require
#ifdef GL_ARB_shader_bit_encoding
#extension GL_ARB_shader_bit_encoding : enable
#endif

uniform 	vec4 _ScreenParams;
uniform 	vec2 _Center;
uniform 	float _Distortion;
uniform 	float _DarkRange;
uniform  sampler2D _MainTex;
in  vec2 vs_TEXCOORD0;
layout(location = 0) out vec4 SV_TARGET0;
vec2 u_xlat0;
vec3 u_xlat1;
vec4 u_xlat10_1;
vec2 u_xlat4;
float u_xlat6;
void main()
{
    u_xlat0.xy = (-vs_TEXCOORD0.xy) + _Center.xy;
    u_xlat4.x = dot(u_xlat0.xy, u_xlat0.xy);
    u_xlat4.x = inversesqrt(u_xlat4.x);
    u_xlat0.xy = u_xlat4.xx * u_xlat0.xy;
    u_xlat4.xy = vs_TEXCOORD0.xy * _ScreenParams.xy;
    u_xlat4.xy = _Center.xy * _ScreenParams.xy + (-u_xlat4.xy);
    u_xlat4.x = dot(u_xlat4.xy, u_xlat4.xy);
    u_xlat4.x = sqrt(u_xlat4.x);
    u_xlat6 = log2(u_xlat4.x);
    u_xlat4.x = _DarkRange * u_xlat4.x + -1.5;
    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
    u_xlat6 = u_xlat6 * _Distortion;
    u_xlat6 = exp2(u_xlat6);
    u_xlat0.xy = vec2(u_xlat6) * u_xlat0.xy;
    u_xlat1.xy = u_xlat0.xy * vec2(30.0, 30.0);
    u_xlat1.z = (-u_xlat1.y);
    u_xlat0.xy = u_xlat1.xz + vs_TEXCOORD0.xy;
    u_xlat10_1 = texture(_MainTex, u_xlat0.xy);
    SV_TARGET0 = u_xlat4.xxxx * u_xlat10_1;
    return;
}

#endif


-- Hardware tier variant: Tier 1
-- Fragment shader for "glcore":
Shader Disassembly:
// All GLSL source is contained within the vertex program

 }
}
Fallback Off
}