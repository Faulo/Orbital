using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICapturable {
    TeamColor belongsTo { get; set; }
    float worth { get; }

    float WorthForTeam(TeamColor yellow);
}
