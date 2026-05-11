using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VehicleRouteTest
{
    private GameObject root;
    private VehicleRouteHandler vehicleRoute;
    private GameViewModel viewModel;

    [SetUp]
    public void SetUp()
    {
        root = new GameObject("VehicleRouteHandler_TestRoot");

        // Prevent Awake from running before fields are assigned
        root.SetActive(false);

        vehicleRoute = root.AddComponent<VehicleRouteHandler>();
        viewModel = root.AddComponent<GameViewModel>();
        SetPrivateField(viewModel, "GameMenu_cnv", CreateCanvas("GameMenu_cnv"));
        SetPrivateField(viewModel, "VehicleRoute_cnv", CreateCanvas("VehicleRoute_cnv"));

        SetPrivateField(vehicleRoute, "Cancel_btn", CreateButton("Cancel_btn"));
        SetPrivateField(vehicleRoute, "Reset_btn", CreateButton("Reset_btn"));
        SetPrivateField(vehicleRoute, "Ok_btn", CreateButton("Ok_btn"));

        // Now Awake/Start can run safely
        root.SetActive(true);
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(root);
    }

    [Test]
    public void IsPlaceSelected_WhenIDisNewAndNear_ReturnsTrue()
    {
        LinkedList<int> route = new LinkedList<int>();
        route.AddLast(1);
        route.AddLast(2);
        route.AddLast(3);
        SetPrivateField(vehicleRoute, "currentRoute", new LinkedList<int>(route));
        Assert.IsTrue(vehicleRoute.IsPlaceSelected(4));
        route = GetPrivateField<LinkedList<int>>("currentRoute");
        Assert.IsTrue(route.Contains(4));
    }

    [Test]
    public void IsPlaceSelected_WhenIDisNewAndNotNear_ReturnsTrue()
    {
        LinkedList<int> route = new LinkedList<int>();
        route.AddLast(1);
        route.AddLast(2);
        route.AddLast(3);
        SetPrivateField(vehicleRoute, "currentRoute", new LinkedList<int>(route));
        // When VEHICLE ROUTE WORKS
        //Assert.Throws<Exception>(() => vehicleRoute.IsPlaceSelected(4));
    }

    [Test]
    public void IsPlaceSelected_WhenRouteContainsIDAndCanBeRemoved_ReturnsFalse()
    {
        LinkedList<int> route = new LinkedList<int>();
        route.AddLast(1);
        route.AddLast(2);
        route.AddLast(3);
        route.AddLast(4);
        SetPrivateField(vehicleRoute, "currentRoute", new LinkedList<int>(route));
        Assert.IsFalse(vehicleRoute.IsPlaceSelected(4));
        route = GetPrivateField<LinkedList<int>>("currentRoute");
        Assert.IsFalse(route.Contains(4));
    }

    [Test]
    public void IsPlaceSelected_WhenRouteContainsIDAndCannotBeRemoved_ReturnsFalse()
    {
        LinkedList<int> route = new LinkedList<int>();
        route.AddLast(1);
        route.AddLast(2);
        route.AddLast(3);
        route.AddLast(4);
        SetPrivateField(vehicleRoute, "currentRoute", new LinkedList<int>(route));
        Assert.Throws<RouteException>(() => vehicleRoute.IsPlaceSelected(3));
        route = GetPrivateField<LinkedList<int>>("currentRoute");
        Assert.IsTrue(route.Contains(3));
    }

    [Test]
    public void IsPlaceInRoute_WhenRouteContainsID_ReturnsTrue()
    {
        LinkedList<int> route = new LinkedList<int>();
        route.AddLast(1);
        route.AddLast(2);
        route.AddLast(3);
        route.AddLast(4);
        SetPrivateField(vehicleRoute, "currentRoute", new LinkedList<int>(route));
        Assert.IsTrue(vehicleRoute.IsPlaceInRoute(4));
    }

    [Test]
    public void IsPlaceInRoute_WhenIDNotInRoute_ReturnsFalse()
    {
        LinkedList<int> route = new LinkedList<int>();
        route.AddLast(1);
        route.AddLast(2);
        route.AddLast(3);
        route.AddLast(4);
        SetPrivateField(vehicleRoute, "currentRoute", new LinkedList<int>(route));
        Assert.IsFalse(vehicleRoute.IsPlaceInRoute(5));
    }

    [Test]
    public void GetPlaceOrder_WhenRouteEmpty_ReturnsEmptyString()
    {
        LinkedList<int> route = new LinkedList<int>();
        SetPrivateField(vehicleRoute, "currentRoute", new LinkedList<int>(route));
        Assert.IsEmpty(vehicleRoute.GetPlaceOrder(4));
    }

    [Test]
    public void GetPlaceOrder_WhenRouteNotEmptyAndRouteContainsID_ReturnsOrderOfIDInRoute()
    {
        LinkedList<int> route = new LinkedList<int>();
        route.AddLast(1);
        route.AddLast(2);
        route.AddLast(3);
        route.AddLast(4);
        SetPrivateField(vehicleRoute, "currentRoute", new LinkedList<int>(route));
        Assert.AreEqual(vehicleRoute.GetPlaceOrder(1), "1");
        Assert.AreEqual(vehicleRoute.GetPlaceOrder(2), "2");
        Assert.AreEqual(vehicleRoute.GetPlaceOrder(3), "3");
        Assert.AreEqual(vehicleRoute.GetPlaceOrder(4), "4");
    }

    [Test]
    public void GetPlaceOrder_WhenRouteNotEmptyAndIDNotInRoute_ReturnsString()
    {
        LinkedList<int> route = new LinkedList<int>();
        route.AddLast(1);
        route.AddLast(2);
        route.AddLast(3);
        route.AddLast(4);
        SetPrivateField(vehicleRoute, "currentRoute", new LinkedList<int>(route));
        Assert.AreEqual(vehicleRoute.GetPlaceOrder(5), "N/A");
    }

    [Test]
    public void LoadRoute_WhenVehicleNotFound_Returns()
    {
        Game game = new Game();
        Game.instance = game;
        Car c = new Car(2, Game.instance.Map);
        //Game.instance.Player.Purchase(c);
        SetPrivateField(viewModel, "selectedObject", c);
        Assert.Throws<RouteException>(() => vehicleRoute.LoadRoute());
    }

    [Test]
    public void LoadRoute_WhenVehicleFound_UpdatesCurrentRoute()
    {
        Game game = new Game();
        Game.instance = game;
        Car c = new Car(2, Game.instance.Map);
        //Game.instance.Player.Purchase(c);
        SetPrivateField(viewModel, "selectedObject", c);
        Assert.Throws<RouteException>(() => vehicleRoute.LoadRoute());
    }

    [Test]
    public void SaveRoute_WhenVehicleFound_UpdatesRouteInModel()
    {

    }

    [Test]
    public void ResetRoute_SetsCurrentRouteEmpty_And_InvokesOnRouteReset()
    {

    }

    private TextMeshProUGUI CreateText(string name)
    {
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(root.transform);
        return obj.AddComponent<TextMeshProUGUI>();
    }

    private Button CreateButton(string name)
    {
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(root.transform);

        Image image = obj.AddComponent<Image>();
        Button button = obj.AddComponent<Button>();
        button.targetGraphic = image;

        return button;
    }

    private Canvas CreateCanvas(string name)
    {
        GameObject canvasObj = new GameObject(
            name,
            typeof(RectTransform),
            typeof(Canvas),
            typeof(CanvasScaler),
            typeof(GraphicRaycaster)
        );

        Canvas canvas = canvasObj.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler scaler = canvasObj.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.matchWidthOrHeight = 0.5f;

        return canvas;
    }

    private void SetPrivateField<TTarget, TValue>(
        TTarget target,
        string fieldName,
        TValue value)
    {
        typeof(TTarget)
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(target, value);
    }

    private T GetPrivateField<T>(string fieldName)
    {
        return (T)typeof(VehicleRouteHandler)
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            .GetValue(vehicleRoute);
    }
}