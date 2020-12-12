---
title: Assets/Plugins/GitHub/Editor/UnityAPIWrapper.cs


---

# Assets/Plugins/GitHub/Editor/UnityAPIWrapper.cs







## Namespaces

| Name           |
| -------------- |
| **[GitHub](Namespaces/namespace_git_hub.md)**  |
| **[GitHub::Unity](Namespaces/namespace_git_hub_1_1_unity.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| class | **[GitHub::Unity::UnityAPIWrapper](Classes/class_git_hub_1_1_unity_1_1_unity_a_p_i_wrapper.md)**  |
















## Source code

```cpp
using UnityEditor;
using UnityEngine;
using System.IO;
using System;

namespace GitHub.Unity
{
    [InitializeOnLoad]
    public class UnityAPIWrapper : ScriptableSingleton<UnityAPIWrapper>
    {
        static UnityAPIWrapper()
        {
#if UNITY_2018_2_OR_NEWER
            Editor.finishedDefaultHeaderGUI += editor => {
                UnityShim.Raise_Editor_finishedDefaultHeaderGUI(editor);
            };
#endif
        }
    }
}
```


-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)
