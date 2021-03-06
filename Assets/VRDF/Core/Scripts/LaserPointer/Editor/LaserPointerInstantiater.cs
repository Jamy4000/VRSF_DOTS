﻿using UnityEngine;
using UnityEditor;

namespace VRDF.Core.LaserPointer
{
    public class LaserPointerInstantiater : Editor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuCommand"></param>
        [MenuItem("GameObject/VRDF/Raycaster, Laser Pointer and VR Clicker/Add Basic Raycaster", priority = 1)]
        [MenuItem("VRDF/Raycaster, Laser Pointer and VR Clicker/Add Basic Raycaster", priority = 1)]
        private static void AddBasicPointer(MenuCommand menuCommand)
        {
            var pointerPrefab = Utils.VRDFPrefabReferencer.GetPrefab("BasicRaycastPointer");
            CreateGameObject(pointerPrefab, menuCommand);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuCommand"></param>
        [MenuItem("GameObject/VRDF/Raycaster, Laser Pointer and VR Clicker/Add Raycaster with Laser Pointer", priority = 1)]
        [MenuItem("VRDF/Raycaster, Laser Pointer and VR Clicker/Add Raycaster with Laser Pointer", priority = 1)]
        private static void AddLaserPointer(MenuCommand menuCommand)
        {
            var pointerPrefab = Utils.VRDFPrefabReferencer.GetPrefab("LaserPointer");
            CreateGameObject(pointerPrefab, menuCommand);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="menuCommand"></param>
        [MenuItem("GameObject/VRDF/Raycaster, Laser Pointer and VR Clicker/Add Raycaster with VR Clicker", priority = 1)]
        [MenuItem("VRDF/Raycaster, Laser Pointer and VR Clicker/Add Raycaster with VR Clicker", priority = 1)]
        private static void AddRaycasterWithClick(MenuCommand menuCommand)
        {
            var pointerPrefab = Utils.VRDFPrefabReferencer.GetPrefab("RaycasterWithClick");
            var newGameObject = CreateGameObject(pointerPrefab, menuCommand);
            var simulatorProxy = newGameObject.GetComponent<Simulator.SimulatorButtonProxyAuthoring>();
            if (simulatorProxy != null)
            {
                simulatorProxy.UseMouseButton = true;
                simulatorProxy.SimulationMouseButton = Simulator.EMouseButton.LEFT_CLICK;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="menuCommand"></param>
        [MenuItem("GameObject/VRDF/Raycaster, Laser Pointer and VR Clicker/Add Raycaster with Laser and VR Clicker", priority = 1)]
        [MenuItem("VRDF/Raycaster, Laser Pointer and VR Clicker/Add Raycaster with Laser and VR Clicker", priority = 1)]
        private static void AddLaserPointerWithClick(MenuCommand menuCommand)
        {
            //Add Raycaster with VR Clicker
            var pointerPrefab = Utils.VRDFPrefabReferencer.GetPrefab("LaserPointerWithClick");
            var newGameObject = CreateGameObject(pointerPrefab, menuCommand);
            var simulatorProxy = newGameObject.GetComponent<Simulator.SimulatorButtonProxyAuthoring>();
            if (simulatorProxy != null)
            {
                simulatorProxy.UseMouseButton = true;
                simulatorProxy.SimulationMouseButton = Simulator.EMouseButton.LEFT_CLICK;
            }
        }

        private static GameObject CreateGameObject(GameObject pointerPrefab, MenuCommand menuCommand)
        {
            // Create a custom game object
            GameObject pointer = PrefabUtility.InstantiatePrefab(pointerPrefab) as GameObject;

            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(pointer, "Create " + pointer.name);

            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(pointer, menuCommand.context as GameObject);

            Selection.activeObject = pointer;

            return pointer;
        }
    }
}