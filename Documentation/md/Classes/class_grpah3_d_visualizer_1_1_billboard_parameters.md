---
title: Grpah3DVisualizer::BillboardParameters
summary: Parameters for creating a new Billboard object.  

---

# Grpah3DVisualizer::BillboardParameters




Parameters for creating a new [Billboard](Classes/class_grpah3_d_visualizer_1_1_billboard.md) object. 
















## Public Functions

|                | Name           |
| -------------- | -------------- |
|  | **[BillboardParameters](Classes/class_grpah3_d_visualizer_1_1_billboard_parameters.md#function-billboardparameters)**([CombinedImages](Classes/class_texture_factory_1_1_combined_images.md) combinedImage, Vector2 scale, float cutoff, bool compressed, bool isMonoColor, Color monoColor) <br>The class constructor.  |


## Public Properties

|                | Name           |
| -------------- | -------------- |
| [CombinedImages](Classes/class_texture_factory_1_1_combined_images.md) | **[CombinedImages](Classes/class_grpah3_d_visualizer_1_1_billboard_parameters.md#property-combinedimages)** <br>Used to set position of images on the [Billboard](Classes/class_grpah3_d_visualizer_1_1_billboard.md).  |
| Vector2 | **[Scale](Classes/class_grpah3_d_visualizer_1_1_billboard_parameters.md#property-scale)** <br>Used to set [Billboard](Classes/class_grpah3_d_visualizer_1_1_billboard.md) size in units.  |
| float | **[Cutoff](Classes/class_grpah3_d_visualizer_1_1_billboard_parameters.md#property-cutoff)** <br>Used to determine the lower bound of the texel clipping, based on the alpha channel summary.  |
| bool | **[Compressed](Classes/class_grpah3_d_visualizer_1_1_billboard_parameters.md#property-compressed)** <br>Used to determine whether the display texture is compressed.  |
| bool | **[IsMonoColor](Classes/class_grpah3_d_visualizer_1_1_billboard_parameters.md#property-ismonocolor)** <br>Used to determine image output mode. If true, image is displayed in one color, false-according to the texture.  |
| Color | **[MonoColor](Classes/class_grpah3_d_visualizer_1_1_billboard_parameters.md#property-monocolor)** <br>Used to determine image color in MonoColor mode.  |



















## Public Functions Documentation

### function BillboardParameters

```cpp
inline BillboardParameters(
    CombinedImages combinedImage,
    Vector2 scale,
    float cutoff,
    bool compressed,
    bool isMonoColor,
    Color monoColor
)
```

The class constructor. 

**Parameters**: 

  * **combinedImage** Used to set position of images on the [Billboard](Classes/class_grpah3_d_visualizer_1_1_billboard.md). 
  * **scale** Used to set [Billboard](Classes/class_grpah3_d_visualizer_1_1_billboard.md) size in units. 
  * **cutoff** Used to determine the lower bound of the texel clipping, based on the alpha channel value. 
  * **compressed** Used to determine whether the display texture is compressed. 
  * **isMonoColor** Used to determine image output mode. If true, image is displayed in one color, false-according to the texture. 
  * **monoColor** Used to determine image color in MonoColor mode.






























## Public Property Documentation

### property CombinedImages

```cpp
CombinedImages CombinedImages;
```

Used to set position of images on the [Billboard](Classes/class_grpah3_d_visualizer_1_1_billboard.md). 




























### property Scale

```cpp
Vector2 Scale;
```

Used to set [Billboard](Classes/class_grpah3_d_visualizer_1_1_billboard.md) size in units. 




























### property Cutoff

```cpp
float Cutoff;
```

Used to determine the lower bound of the texel clipping, based on the alpha channel summary. 




























### property Compressed

```cpp
bool Compressed;
```

Used to determine whether the display texture is compressed. 




























### property IsMonoColor

```cpp
bool IsMonoColor;
```

Used to determine image output mode. If true, image is displayed in one color, false-according to the texture. 




























### property MonoColor

```cpp
Color MonoColor;
```

Used to determine image color in MonoColor mode. 


































-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)