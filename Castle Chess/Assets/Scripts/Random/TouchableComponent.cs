using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TouchableComponent
{
    void ClickDown();

    void ClickHeld();

    void ClickUp();
}
