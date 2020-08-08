using UnityEngine;
using UnityEngine.AI;

namespace Sweet_And_Salty_Studios
{
    public enum UNIT_ACTION
    {
        MOVE,
        ATTACK,
        STOP,
        HOLD,
        PATROL
    }

    public abstract class Unit : MonoBehaviour
    {
        protected string unitName;

        private UNIT_ACTION currentUnitAction;

        private NavMeshAgent navMeshAgent;

        public MeshRenderer MeshRenderer
        {
            get;
            private set;
        }

        private Color defaultColor, activeColor;

        private PlayerEngine owner;

        protected int maxHealth = 100;
        protected int currentHealth;
        protected int armorClass = 0;
        protected float speed = 3.5f;

        protected virtual void Awake()
        {
            MeshRenderer = GetComponentInChildren<MeshRenderer>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        protected virtual void Start()
        {
            defaultColor = MeshRenderer.material.color;
            activeColor = Color.red;

            gameObject.name = unitName;
        }

        public virtual void Initialize(
            PlayerEngine owner,
            int maxHealth,
            int armorClass,
            float speed)
        {
            this.owner = owner;
            this.maxHealth = maxHealth;
            this.armorClass = armorClass;
            this.speed = speed;

            navMeshAgent.speed = speed;
            currentHealth = maxHealth;         
        }

        private void ChangeAction(UNIT_ACTION newUnitAction)
        {
            currentUnitAction = newUnitAction;

            switch (currentUnitAction)
            {
                case UNIT_ACTION.MOVE:

                    break;

                case UNIT_ACTION.ATTACK:

                    break;

                case UNIT_ACTION.STOP:

                    break;

                case UNIT_ACTION.HOLD:

                    break;

                case UNIT_ACTION.PATROL:

                    break;

                default:

                    break;
            }
        }

        public void Move(Vector3 newPosition)
        {
            if (navMeshAgent.isStopped)
                navMeshAgent.isStopped = false;

            navMeshAgent.SetDestination(newPosition);
        }

        public void Attack()
        {

        }

        public void Stop()
        {
            navMeshAgent.isStopped = true;
        }

        public void OnSelected()
        {
            MeshRenderer.material.color = activeColor;
        }

        public void OnDeselected()
        {
            MeshRenderer.material.color = defaultColor;
        }
    }
}