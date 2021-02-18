---
title: Graph3D::SurrogateTypesForSerialization::NewtonsoftSurrogateConverter

---

# Graph3D::SurrogateTypesForSerialization::NewtonsoftSurrogateConverter



 [More...](#detailed-description)

Inherits from JsonConverter

## Public Functions

|                | Name           |
| -------------- | -------------- |
| override bool | **[CanConvert](Classes/class_graph3_d_1_1_surrogate_types_for_serialization_1_1_newtonsoft_surrogate_converter.md#function-canconvert)**(Type objectType) |
| override object | **[ReadJson](Classes/class_graph3_d_1_1_surrogate_types_for_serialization_1_1_newtonsoft_surrogate_converter.md#function-readjson)**(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) |
| override void | **[WriteJson](Classes/class_graph3_d_1_1_surrogate_types_for_serialization_1_1_newtonsoft_surrogate_converter.md#function-writejson)**(JsonWriter writer, object value, JsonSerializer serializer) |

## Detailed Description

```cpp
template <TReal ,
TSurrogate >
class Graph3D::SurrogateTypesForSerialization::NewtonsoftSurrogateConverter;
```

## Public Functions Documentation

### function CanConvert

```cpp
override bool CanConvert(
    Type objectType
)
```


### function ReadJson

```cpp
override object ReadJson(
    JsonReader reader,
    Type objectType,
    object existingValue,
    JsonSerializer serializer
)
```


### function WriteJson

```cpp
override void WriteJson(
    JsonWriter writer,
    object value,
    JsonSerializer serializer
)
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)