using System;
using System.Collections.Generic;
using UnityEngine;

public enum Statistic
{
    Life,
    Damage,
    Armor
}

[Serializable]
public class StatsValue
{
    public Statistic statType;
    public int value;

    public StatsValue(Statistic statType, int value = 0)
    {
        this.statType = statType;
        this.value = value;
    }
}

[Serializable]
public class StatsGroup
{
    public List<StatsValue> stats;

    public StatsGroup()
    {
        stats = new List<StatsValue>();
        Init();
    }

    public void Init()
    {
        stats.Add(new StatsValue(Statistic.Life, 100));
        stats.Add(new StatsValue(Statistic.Damage, 25));
        stats.Add(new StatsValue(Statistic.Armor, 5));
    }

    internal StatsValue Get(Statistic statistic)
    {
        return stats[(int)statistic];
    }
}

public enum Atribute
{
    Strenght,
    Dexterity,
    Intelligence
}

[Serializable]
public class AtributeValue
{
    public Atribute atributeType;
    public int value;

    public AtributeValue(Atribute atributeType, int value = 0)
    {
        this.atributeType = atributeType;
        this.value = value;
    }
}

[Serializable]
public class AtributeGroup
{
    public List<AtributeValue> atributeValues;

    public AtributeGroup()
    {
        atributeValues = new List<AtributeValue>();
        Init();
    }

    public void Init()
    {
        atributeValues.Add(new AtributeValue(Atribute.Strenght));
        atributeValues.Add(new AtributeValue(Atribute.Dexterity));
        atributeValues.Add(new AtributeValue(Atribute.Intelligence));
    }
}

[Serializable]
public class ValuePool
{
    public StatsValue maxValue;
    public int currentValue;

    public ValuePool(StatsValue maxValue)
    {
        this.maxValue = maxValue;
        this.currentValue = maxValue.value;
    }
}

public class Character : MonoBehaviour
{
    [SerializeField] AtributeGroup attributes;
    [SerializeField] StatsGroup stats;
    [SerializeField] ValuePool lifePool;

    private void Start()
    {
        attributes = new AtributeGroup();
        attributes.Init();
        stats = new StatsGroup();
        stats.Init();
        lifePool = new ValuePool(TakeStats(Statistic.Life));
    }

    public void TakeDamage(int damage)
    {
        damage = ApplyDefences(damage);

        lifePool.currentValue -= damage;
        CheckDeath();
    }

    public void CheckDeath()
    {
        if (lifePool.currentValue <= 0)
        {
            Destroy(gameObject);
        }
    }

    private int ApplyDefences(int damage)
    {
        damage -= TakeStats(Statistic.Armor).value;
        if(damage < 0)
        {
            damage = 0;
        }
        return damage;
    }

    public StatsValue TakeStats(Statistic statistic)
    {
        return stats.Get(statistic);
    }
}
