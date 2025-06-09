namespace Units.Implementation
{
    public class PooledEnemyUnit : PooledUnitBase
    {
        private PooledEnemyUnitDataSO unitDataSO;

        public override void ResetUnit()
        {
            damageable?.ResetDamagable();
        }
        public override void SetUnitData(UnitDataSO unitDataSO)
        {
            base.SetUnitData(unitDataSO);

            this.unitDataSO = unitDataSO as PooledEnemyUnitDataSO;

            damageable?.Launch(unitDataSO.DamagableDataSO);

            characterController.SetData(unitDataSO.CharacterControllerDataSO);
        }
    }
}
