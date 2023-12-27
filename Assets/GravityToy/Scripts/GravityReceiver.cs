using System.Collections;
using UnityEngine;

namespace GravityToy
{
    public class GravityReceiver : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;

        public void ReceiveInfluence(GravityWell well)
        {
            Vector3 toWell = well.Position - transform.position;
            _rigidbody.AddForce(toWell.normalized * well.Strength);
        }
    }
}