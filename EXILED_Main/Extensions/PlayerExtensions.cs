﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EXILED.Extensions
{
    public static class PlayerExtensions
    {
        /// <summary>
        /// Sets the position of a <see cref="ReferenceHub"/> using a <see cref="Vector3"/>.
        /// </summary>
        public static void SetPosition(this ReferenceHub rh, Vector3 position) => rh.plyMovementSync.OverridePosition(position, rh.transform.rotation.eulerAngles.y);
        /// <summary>
        /// Sets the position of a <see cref="ReferenceHub"/> using the x, y, and z of the destination position.
        /// </summary>
        public static void SetPosition(this ReferenceHub rh, float x, float y, float z) => rh.plyMovementSync.OverridePosition(new Vector3(x, y, z), rh.transform.rotation.eulerAngles.y);
        /// <summary>
        /// Sets the rotation of a <see cref="ReferenceHub"/> using a <see cref="Vector2"/>.
        /// </summary>
        public static void SetRotation(this ReferenceHub rh, Vector2 rotations) => rh.plyMovementSync.NetworkRotations = rotations;
        /// <summary>
        /// Sets the rotation of a <see cref="ReferenceHub"/> using the x and y values of the desired rotation.
        /// </summary>
        public static void SetRotation(this ReferenceHub rh, float x, float y) => rh.plyMovementSync.NetworkRotations = new Vector2(x, y);
        /// <summary>
        /// Sets the rank color of a <see cref="ReferenceHub"/> to a given color with a <see cref="string"/>.
        /// </summary>
        public static void SetRankColor(this ReferenceHub rh, string color) => rh.serverRoles.SetColor(color);
        /// <summary>
        /// Sets the rank name of a <see cref="ReferenceHub"/> to a given <see cref="string"/>.
        /// </summary>
        public static void SetRankName(this ReferenceHub rh, string name) => rh.serverRoles.SetText(name);
        /// <summary>
        /// Sets the rank of a <see cref="ReferenceHub"/> by giving a <paramref name="name"/>, <paramref name="color"/>, and setting if it should be shown with <paramref name="show"/>.
        /// </summary>
        public static void SetRole(this ReferenceHub rh, string name, string color, bool show)
        {
            // Developer note: I bet I just needed to use the show once. But hey, better be safe than sorry.
            var ug = new UserGroup()
            {
                BadgeColor = color,
                BadgeText = name,
                HiddenByDefault = !show,
                Cover = show
            };
            rh.serverRoles.SetGroup(ug, false, false, show);
        }
        /// <summary>
        /// Sets the rank of a <see cref="ReferenceHub"/> to a <see cref="UserGroup"/>.
        /// </summary>
        public static void SetRole(this ReferenceHub rh, UserGroup userGroup) => rh.serverRoles.SetGroup(userGroup, false, false, false);
        public static void SetNickname(this ReferenceHub rh, string name) => rh.nicknameSync.Network_myNickSync = name;

        // Adapted from https://github.com/galaxy119/SamplePlugin/blob/master/SamplePlugin/Extensions.cs
        public static void RAMessage(this CommandSender sender, string message, bool success = true, string pluginName = null) =>
            sender.RaReply((pluginName ?? Assembly.GetCallingAssembly().FullName) + "#" + message, success, true, string.Empty);

        public static void Broadcast(this ReferenceHub rh, uint time, string message) => rh.GetComponent<Broadcast>().TargetAddElement(rh.scp079PlayerScript.connectionToClient, message, time, false);
    }
}