using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {
    TeamColor teamColor { get; }
    void TakeDamage(float value);
}
