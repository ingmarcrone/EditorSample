using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RestorePointManager : SpriteRenderersManager_Base
{
    private Type[] _acceptableRestorePointTypes = new Type[]
    {
        typeof(RestorePoint_Position),
        typeof(RestorePoint_Rotation),
        typeof(RestorePoint_OnOff),
        typeof(RestorePoint_Scale),
        typeof(RestorePoint_SortingOrder),
        typeof(RestorePoint_Materials),
    };


    #region RestorePoint_Properties_Container

    private Dictionary<Type, List<RestorePoint_Property_Base>> _restorePoint_Properties_Container;
    private Dictionary<Type, List<RestorePoint_Property_Base>> RestorePoint_Properties_Container
    {
        get
        {
            if (_restorePoint_Properties_Container == null)
                Instantiate_RestorePoint_Properties_Container();
            return _restorePoint_Properties_Container;
        }
        set => _restorePoint_Properties_Container = value;
    }

    private bool IsRestorePoint_Properties_Container_Instantiated()
    {
        if (RestorePoint_Properties_Container == null)
            return false;

        foreach (Type type in _acceptableRestorePointTypes)
        {
            if (!RestorePoint_Properties_Container.ContainsKey(type))
                return false;

            if (RestorePoint_Properties_Container[type] == null)
                return false;
        }

        return true;
    }

    #endregion


    #region RestorePoint_Properties_Container_Initial

    public void Populate_RestorePoint_Properties_Initial()
    {
        Instantiate_RestorePoint_Properties_Container();
        Populate_RestorePoint_Properties();
        InitialCleanup();
        UpdateReport();
    }

    private void Instantiate_RestorePoint_Properties_Container()
    {
        _restorePoint_Properties_Container = new Dictionary<Type, List<RestorePoint_Property_Base>>();
        foreach (Type type in _acceptableRestorePointTypes)
            NewListForType(type);
    }

    private void NewListForType(Type type)
    {
        if (!_restorePoint_Properties_Container.ContainsKey(type))
            _restorePoint_Properties_Container.Add(type, new List<RestorePoint_Property_Base>());
        else
            _restorePoint_Properties_Container[type] = new List<RestorePoint_Property_Base>();
    }

    private void Populate_RestorePoint_Properties()
    {
        foreach (SpriteRenderer spriteRenderer in GetSpriteRenderers())
        {
            foreach (Type acceptableType in _acceptableRestorePointTypes)
            {
                Component component = FindPropertyComponent_OfType_InSpriteRenderer(acceptableType, spriteRenderer);
                if (component != null)
                    RestorePoint_Properties_Container[acceptableType].Add((RestorePoint_Property_Base)component);
            }
        }
    }

    private void InitialCleanup()
    {
        foreach (Type acceptableType in _acceptableRestorePointTypes)
            RemoveRestorePoints_OfType_FromAllSpriteRenderers(acceptableType);
    }

    #endregion


    #region AddRemove_RestorePoints_OfType_ToAllSpriteRenderers

    private Type lastCalledType;
    public void AddRestorePoints_OfType_ToAllSpriteRenderers(Type type)
    {
        if (type == null)
            return;

        type = ConvertToAcceptableType(type);

        if (!IsTypeAcceptable(type))
            return;

        lastCalledType = type;
        RemoveRestorePoints_OfType_FromAllSpriteRenderers(lastCalledType);

        foreach (SpriteRenderer spriteRenderer in GetSpriteRenderers())
        {
            RestorePoint_Property_Base propertyComponent = (RestorePoint_Property_Base)FindOrAdd_Property_Component(type, spriteRenderer);
            propertyComponent.Store();
            Add_PropertyComponent_To_RestorePoint_Properties_Container(type, propertyComponent);
        }

        UpdateReport();
    }

    private void Add_PropertyComponent_To_RestorePoint_Properties_Container(Type type, RestorePoint_Property_Base component)
    {
        if (RestorePoint_Properties_Container == null)
            Instantiate_RestorePoint_Properties_Container();

        if (RestorePoint_Properties_Container.ContainsKey(type))
            RestorePoint_Properties_Container[type].Add(component);
    }

    private static Component FindOrAdd_Property_Component(Type type, SpriteRenderer spriteRenderer)
    {
        var component = FindPropertyComponent_OfType_InSpriteRenderer(type, spriteRenderer);
        if (component == null)
            component = AddPropertyComponent_OfType_ToSpriteRenderer(type, spriteRenderer);
        return component;
    }

    private static Component AddPropertyComponent_OfType_ToSpriteRenderer(Type type, SpriteRenderer spriteRenderer) => spriteRenderer.gameObject.AddComponent(type) as RestorePoint_Property_Base;

    private static Component FindPropertyComponent_OfType_InSpriteRenderer(Type type, SpriteRenderer spriteRenderer) => spriteRenderer.transform.GetComponent(type);

    public void RemoveRestorePoints_OfType_FromAllSpriteRenderers(Type type, bool isDeleteAnyway = false)
    {
        if (type == null)
            return;

        type = ConvertToAcceptableType(type);

        if (!IsTypeAcceptable(type))
            return;

        if (isDeleteAnyway)
        {
            DeleteAlInList(RestorePoint_Properties_Container[type]);
            NewListForType(type);
        }

        List<RestorePoint_Property_Base> propertiesAangepast = new List<RestorePoint_Property_Base>();
        List<RestorePoint_Property_Base> propertiesNietAangepast = new List<RestorePoint_Property_Base>();
        VerzamelWelEnNietAangepast(type, propertiesAangepast, propertiesNietAangepast);

        RestorePoint_Properties_Container[type] = propertiesAangepast;

        DeleteAlInList(propertiesNietAangepast);

        UpdateReport();
    }

    private void VerzamelWelEnNietAangepast(Type type, List<RestorePoint_Property_Base> propertiesAangepast, List<RestorePoint_Property_Base> propertiesNietAangepast)
    {
        if (RestorePoint_Properties_Container.ContainsKey(type))
        {
            foreach (var component in RestorePoint_Properties_Container[type])
            {
                if (component.IsAangepast())
                    propertiesAangepast.Add(component);
                else
                    propertiesNietAangepast.Add(component);
            }
        }
    }

    private void DeleteAlInList(List<RestorePoint_Property_Base> components)
    {
        foreach (var component in components)
            DestroyImmediate(component);
        components = null;
    }

    private bool IsTypeAcceptable(Type type) => _acceptableRestorePointTypes.Any(x => x == type);

    private Type ConvertToAcceptableType(Type type)
    {
        if (!IsTypeAcceptable(type))
            type = TypeHelper.FindMatchingType(type);
        return type;
    }

    #endregion


    #region Restore_RestorePoints_OfType_ToAllSpriteRenderers

    public void Restore_RestorePoints_OfType_ToAllSpriteRenderers(Type type)
    {
        if (type == null)
            return;

        type = ConvertToAcceptableType(type);

        if (!IsTypeAcceptable(type))
            return;

        if (!IsRestorePoint_Properties_Container_Instantiated())
            Instantiate_RestorePoint_Properties_Container();

        if (RestorePoint_Properties_Container[type].Count < 1)
            return;

        foreach (var r in RestorePoint_Properties_Container[type])
            r.Restore();

        RemoveRestorePoints_OfType_FromAllSpriteRenderers(type);

        UpdateReport();
    }

    #endregion


    #region Report

    private Dictionary<Type, int> _report;
    private Dictionary<Type, int> Report
    {
        get
        {
            if (_report == null)
                Instantiate_Report();
            return _report;
        }
        set => _report = value;
    }

    private void Instantiate_Report()
    {
        _report = new Dictionary<Type, int>();
        foreach (Type type in _acceptableRestorePointTypes)
            _report.Add(type, 0);
    }

    private bool IsReport_Instantiated()
    {
        if (Report == null)
            return false;

        foreach (Type type in _acceptableRestorePointTypes)
            if (!Report.ContainsKey(type))
                return false;

        return true;
    }

    public void UpdateReport()
    {
        if (!IsReport_Instantiated())
            Instantiate_Report();

        ResetReport();

        if (!IsRestorePoint_Properties_Container_Instantiated())
            return;

        foreach (var restorePoint_Property in RestorePoint_Properties_Container)
            Report[restorePoint_Property.Key] = restorePoint_Property.Value.Where(x => x.IsAangepast()).Count();
    }

    private void ResetReport()
    {
        foreach (Type type in _acceptableRestorePointTypes)
            Report[type] = 0;
    }

    public int GetReportForType(Type type)
    {
        if (!IsReport_Instantiated())
            Instantiate_Report();

        if (!IsTypeAcceptable(type))
            type = ConvertToAcceptableType(type);

        if (Report.ContainsKey(type))
            return Report[type];

        return 0;
    }

    #endregion













}
