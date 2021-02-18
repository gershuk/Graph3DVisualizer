---
title: Graph3DVisualizer::TextureFactory::PositionedImage
summary: Class that describes an image with a 2 dimensional coordinate reference. 

---

# Graph3DVisualizer::TextureFactory::PositionedImage



Class that describes an image with a 2 dimensional coordinate reference. ## Public Functions

|                | Name           |
| -------------- | -------------- |
| | **[PositionedImage](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_positioned_image.md#function-positionedimage)**(Texture2D texture, Vector2Int position) |
| void | **[Deconstruct](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_positioned_image.md#function-deconstruct)**(out Texture2D texture, out Vector2Int position) |
| override bool | **[Equals](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_positioned_image.md#function-equals)**(object obj) |
| override int | **[GetHashCode](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_positioned_image.md#function-gethashcode)**() |
| implicit | **[operator](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_positioned_image.md#function-operator)**(Texture2D Texture, Vector2Int Position) |
| implicit | **[operator PositionedImage](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_positioned_image.md#function-operator-positionedimage)**((Texture2D Texture, Vector2Int Position) value) |

## Public Attributes

|                | Name           |
| -------------- | -------------- |
| Vector2Int | **[Position](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_positioned_image.md#variable-position)**  |
| Texture2D | **[Texture](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_positioned_image.md#variable-texture)**  |

## Public Functions Documentation

### function PositionedImage

```cpp
inline PositionedImage(
    Texture2D texture,
    Vector2Int position
)
```


### function Deconstruct

```cpp
inline void Deconstruct(
    out Texture2D texture,
    out Vector2Int position
)
```


### function Equals

```cpp
override bool Equals(
    object obj
)
```


### function GetHashCode

```cpp
inline override int GetHashCode()
```


### function operator

```cpp
static implicit operator(
    Texture2D Texture,
    Vector2Int Position
)
```


### function operator PositionedImage

```cpp
static implicit operator PositionedImage(
    (Texture2D Texture, Vector2Int Position) value
)
```


## Public Attributes Documentation

### variable Position

```cpp
Vector2Int Position;
```


### variable Texture

```cpp
Texture2D Texture;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)