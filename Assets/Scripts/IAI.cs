using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAI
{
    IEnumerator Move(List<Node> path);
}
