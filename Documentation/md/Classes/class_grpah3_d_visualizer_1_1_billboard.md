---
title: Grpah3DVisualizer::Billboard
summary: An object containing information for displaying the Billboard image. Contained in the BillboardController.  

---

# Grpah3DVisualizer::Billboard




An object containing information for displaying the [Billboard]() image. Contained in the [BillboardController](). 



Inherits from [SupportComponents.ICustomizable< BillboardParameters >](Classes/interface_support_components_1_1_i_customizable.md)













## Public Functions

|                | Name           |
| -------------- | -------------- |
| void | **[SetupParams](Classes/class_grpah3_d_visualizer_1_1_billboard.md#function-setupparams)**([BillboardParameters](Classes/class_grpah3_d_visualizer_1_1_billboard_parameters.md) billboardParameters)  |
| [BillboardParameters](Classes/class_grpah3_d_visualizer_1_1_billboard_parameters.md) | **[DownloadParams](Classes/class_grpah3_d_visualizer_1_1_billboard.md#function-downloadparams)**()  |
|  | **[Billboard](Classes/class_grpah3_d_visualizer_1_1_billboard.md#function-billboard)**(in [BillboardParameters](Classes/class_grpah3_d_visualizer_1_1_billboard_parameters.md) parameters, Shader shader, Texture2D defaultTexture)  |


## Public Properties

|                | Name           |
| -------------- | -------------- |
| Material | **[Material](Classes/class_grpah3_d_visualizer_1_1_billboard.md#property-material)**  |
| Texture | **[MainTexture](Classes/class_grpah3_d_visualizer_1_1_billboard.md#property-maintexture)**  |
| float? | **[ScaleX](Classes/class_grpah3_d_visualizer_1_1_billboard.md#property-scalex)**  |
| float? | **[ScaleY](Classes/class_grpah3_d_visualizer_1_1_billboard.md#property-scaley)**  |
| float | **[Cutoff](Classes/class_grpah3_d_visualizer_1_1_billboard.md#property-cutoff)**  |
| Vector2 | **[TextureOffset](Classes/class_grpah3_d_visualizer_1_1_billboard.md#property-textureoffset)**  |
| Vector2 | **[TextureScale](Classes/class_grpah3_d_visualizer_1_1_billboard.md#property-texturescale)**  |
| bool | **[IsMonoColor](Classes/class_grpah3_d_visualizer_1_1_billboard.md#property-ismonocolor)**  |
| Color | **[MonoColor](Classes/class_grpah3_d_visualizer_1_1_billboard.md#property-monocolor)**  |


## Public Attributes

|                | Name           |
| -------------- | -------------- |
| Action | **[ScaleChanged](Classes/class_grpah3_d_visualizer_1_1_billboard.md#variable-scalechanged)**  |

















## Public Functions Documentation

### function SetupParams

```cpp
inline void SetupParams(
    BillboardParameters billboardParameters
)
```





























### function DownloadParams

```cpp
BillboardParameters DownloadParams()
```





























### function Billboard

```cpp
inline Billboard(
    in BillboardParameters parameters,
    Shader shader,
    Texture2D defaultTexture
)
```































## Public Property Documentation

### property Material

```cpp
Material Material;
```





























### property MainTexture

```cpp
Texture MainTexture;
```





























### property ScaleX

```cpp
float? ScaleX;
```





























### property ScaleY

```cpp
float? ScaleY;
```





























### property Cutoff

```cpp
float Cutoff;
```





























### property TextureOffset

```cpp
Vector2 TextureOffset;
```





























### property TextureScale

```cpp
Vector2 TextureScale;
```





























### property IsMonoColor

```cpp
bool IsMonoColor;
```





























### property MonoColor

```cpp
Color MonoColor;
```































## Public Attributes Documentation

### variable ScaleChanged

```cpp
Action ScaleChanged;
```

































-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)