using UnityEngine;

public class TestStateContext
{
    private Rigidbody rigidBody;

    public TestStateContext(Rigidbody rigidBody)
    {
        this.rigidBody = rigidBody;
    }

    public Rigidbody Rb => rigidBody;
}
