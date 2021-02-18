---
title: Graph3DVisualizer::Billboards::Billboard
summary: An object containing information for displaying the Billboard image. Contained in the BillboardController. 

---

# Graph3DVisualizer::Billboards::Billboard



An object containing information for displaying the [Billboard]() image. Contained in the [BillboardController](). Inherits from [Graph3DVisualizer.Customizable.ICustomizable< BillboardParameters >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md)

## Public Events

|                | Name           |
| -------------- | -------------- |
| Action | **[ScaleChanged](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard.md#event-scalechanged)**() |

## Public Functions

|                | Name           |
| -------------- | -------------- |
| | **[Billboard](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard.md#function-billboard)**(in [BillboardParameters](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_parameters.md) parameters, Shader shader, Texture2D defaultTexture) |
| [BillboardParameters](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_parameters.md) | **[DownloadParams](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard.md#function-downloadparams)**() |
| void | **[SetupParams](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard.md#function-setupparams)**([BillboardParameters](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_parameters.md) billboardParameters) |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| float | **[Cutoff](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard.md#property-cutoff)**  |
| bool | **[IsMonoColor](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard.md#property-ismonocolor)**  |
| Texture | **[MainTexture](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard.md#property-maintexture)**  |
| Material | **[Material](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard.md#property-material)**  |
| Color | **[MonoColor](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard.md#property-monocolor)**  |
| float? | **[ScaleX](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard.md#property-scalex)**  |
| float? | **[ScaleY](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard.md#property-scaley)**  |
| Vector2 | **[TextureOffset](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard.md#property-textureoffset)**  |
| Vector2 | **[TextureScale](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard.md#property-texturescale)**  |

## Public Events Documentation

### event ScaleChanged

```cpp
Action ScaleChanged()
```


## Public Functions Documentation

### function Billboard

```cpp
inline Billboard(
    in BillboardParameters parameters,
    Shader shader,
    Texture2D defaultTexture
)
```


### function DownloadParams

```cpp
BillboardParameters DownloadParams()
```


### function SetupParams

```cpp
inline void SetupParams(
    BillboardParameters billboardParameters
)
```


## Public Property Documentation

### property Cutoff

```cpp
float Cutoff;
```


### property IsMonoColor

```cpp
bool IsMonoColor;
```


### property MainTexture

```cpp
Texture MainTexture;
```


### property Material

```cpp
Material Material;
```


### property MonoColor

```cpp
Color MonoColor;
```


### property ScaleX

```cpp
float? ScaleX;
```


### property ScaleY

```cpp
float? ScaleY;
```


### property TextureOffset

```cpp
Vector2 TextureOffset;
```


### property TextureScale

```cpp
Vector2 TextureScale;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)