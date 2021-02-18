---
title: Graph3DVisualizer::Customizable::CustomizableExtension
summary: A class containing functions for dynamically calling methods ICustomizable<TParams>.DownloadParams, ICustomizable<TParams>.SetupParams(TParams). 

---

# Graph3DVisualizer::Customizable::CustomizableExtension



A class containing functions for dynamically calling methods ICustomizable<TParams>.DownloadParams, ICustomizable<TParams>.SetupParams(TParams). ## Public Functions

|                | Name           |
| -------------- | -------------- |
| [AbstractCustomizableParameter](Classes/class_graph3_d_visualizer_1_1_customizable_1_1_abstract_customizable_parameter.md) | **[CallDownloadParams](Classes/class_graph3_d_visualizer_1_1_customizable_1_1_customizable_extension.md#function-calldownloadparams)**(object customizable) |
| List< T > | **[CallDownloadParams< T >](Classes/class_graph3_d_visualizer_1_1_customizable_1_1_customizable_extension.md#function-calldownloadparams<-t->)**(object customizable) |
| void | **[CallSetUpParams](Classes/class_graph3_d_visualizer_1_1_customizable_1_1_customizable_extension.md#function-callsetupparams)**(object customizable, object parameter) |
| void | **[CallSetUpParams](Classes/class_graph3_d_visualizer_1_1_customizable_1_1_customizable_extension.md#function-callsetupparams)**(object customizable, object[] parameters) |

## Public Functions Documentation

### function CallDownloadParams

```cpp
static inline AbstractCustomizableParameter CallDownloadParams(
    object customizable
)
```


### function CallDownloadParams< T >

```cpp
static inline List< T > CallDownloadParams< T >(
    object customizable
)
```


### function CallSetUpParams

```cpp
static inline void CallSetUpParams(
    object customizable,
    object parameter
)
```


### function CallSetUpParams

```cpp
static inline void CallSetUpParams(
    object customizable,
    object[] parameters
)
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)