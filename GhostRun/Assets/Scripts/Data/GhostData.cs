using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostData
{
    public GhostTemplate template { get; private set; }
    public int id { get; private set; }
    public float power { get; private set; }

    public GhostData(GhostTemplate template)
    {
        this.template = template;
        this.power = template.power;
    }
}
