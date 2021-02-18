---
title: Graph3DVisualizer::TextureFactory::CombinedImages
summary: Class describing an image consisting of several pictures. 

---

# Graph3DVisualizer::TextureFactory::CombinedImages



Class describing an image consisting of several pictures. Inherits from ICloneable

## Public Functions

|                | Name           |
| -------------- | -------------- |
| | **[CombinedImages](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_combined_images.md#function-combinedimages)**([PositionedImage](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_positioned_image.md)[] images, int textureWidth, int textureHeight, TextureWrapMode wrapMode, bool isTransparentBackground) |
| object | **[Clone](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_combined_images.md#function-clone)**() |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| [PositionedImage](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_positioned_image.md)[] | **[Images](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_combined_images.md#property-images)**  |
| bool | **[IsTransparentBackground](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_combined_images.md#property-istransparentbackground)**  |
| int | **[TextureHeight](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_combined_images.md#property-textureheight)**  |
| int | **[TextureWidth](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_combined_images.md#property-texturewidth)**  |
| TextureWrapMode | **[WrapMode](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_combined_images.md#property-wrapmode)**  |

## Public Functions Documentation

### function CombinedImages

```cpp
inline CombinedImages(
    PositionedImage[] images,
    int textureWidth,
    int textureHeight,
    TextureWrapMode wrapMode,
    bool isTransparentBackground
)
```


### function Clone

```cpp
object Clone()
```


## Public Property Documentation

### property Images

```cpp
PositionedImage[] Images;
```


### property IsTransparentBackground

```cpp
bool IsTransparentBackground;
```


### property TextureHeight

```cpp
int TextureHeight;
```


### property TextureWidth

```cpp
int TextureWidth;
```


### property WrapMode

```cpp
TextureWrapMode WrapMode;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)