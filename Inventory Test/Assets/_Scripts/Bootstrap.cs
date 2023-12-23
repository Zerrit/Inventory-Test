using System;
using _Scripts.InventorySystem;
using _Scripts.InventorySystem.Factory;
using _Scripts.InventorySystem.View;
using UnityEngine;

namespace _Scripts
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private InventorySystemView view;

        private IItemFactory _factory;
        private InventorySystemController _controller;

        private void Awake()
        {
            _factory = new ItemFactory();
            _controller = new InventorySystemController();
            view.Initialize(_controller, _factory);
        }

        private void Start()
        {
            _controller.OpenInventory();
            Debug.Log("Работа приложения запущена");
        }

        private void OnDestroy()
        {
            _controller.CloseInventory();
            Debug.Log("Работа приложения завешена");
        }
    }
}