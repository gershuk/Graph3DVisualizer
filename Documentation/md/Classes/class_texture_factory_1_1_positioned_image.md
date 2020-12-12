---
title: TextureFactory::PositionedImage


---

# TextureFactory::PositionedImage





















## Public Functions

|                | Name           |
| -------------- | -------------- |
|  | **[PositionedImage](Classes/class_texture_factory_1_1_positioned_image.md#function-positionedimage)**(Texture2D texture, Vector2Int position)  |
| override bool | **[Equals](Classes/class_texture_factory_1_1_positioned_image.md#function-equals)**(object obj)  |
| override int | **[GetHashCode](Classes/class_texture_factory_1_1_positioned_image.md#function-gethashcode)**()  |
| void | **[Deconstruct](Classes/class_texture_factory_1_1_positioned_image.md#function-deconstruct)**(out Texture2D texture, out Vector2Int position)  |
| implicit | **[operator](Classes/class_texture_factory_1_1_positioned_image.md#function-operator)**(Texture2D Texture, Vector2Int Position)  |
| implicit | **[operator PositionedImage](Classes/class_texture_factory_1_1_positioned_image.md#function-operator-positionedimage)**((Texture2D [Texture](Classes/class_texture_factory_1_1_positioned_image.md#variable-texture), Vector2Int [Position](Classes/class_texture_factory_1_1_positioned_image.md#variable-position)) value)  |




## Public Attributes

|                | Name           |
| -------------- | -------------- |
| Texture2D | **[Texture](Classes/class_texture_factory_1_1_positioned_image.md#variable-texture)**  |
| Vector2Int | **[Position](Classes/class_texture_factory_1_1_positioned_image.md#variable-position)**  |

















## Public Functions Documentation

### function PositionedImage

```cpp
inline PositionedImage(
    Texture2D texture,
    Vector2Int position
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





























### function Deconstruct

```cpp
inline void Deconstruct(
    out Texture2D texture,
    out Vector2Int position
)
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

### variable Texture

```cpp
Texture2D Texture;
```





























### variable Position

```cpp
Vector2Int Position;
```

































-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)