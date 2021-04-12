﻿namespace Turbo.plugins.patrick.skills.definitions.player
{
    using System.Collections.Generic;
    using parameters;
    using parameters.types;
    using Plugins;
    using Plugins.Patrick.forms;

    public class IsBuffActive : AbstractDefinition
    {
        public string buffName { get; set; }

        public uint SelectedSno { get; set; }
        
        public bool OverrideSelectedWithSno { get; set; }
        
        public uint Sno { get; set; }
        
        public override DefinitionType category
        {
            get
            {
                return DefinitionType.Player;
            }
        }
        public override string attributes
        {
            get
            {
                return $"[ buff: {buffName} ]";
            }
        }

        public override List<AbstractParameter> GetParameters(IController hud)
        {
            return new List<AbstractParameter>
            {
                ContextParameter.of(
                    nameof(buffName),
                    input =>
                    {
                        if (!(input is KeyValuePair<string, ISnoPower> pair))
                            return;
                        buffName = pair.Key;
                        SelectedSno = pair.Value.Sno;
                    },
                    Settings.NameToSnoPower,
                    "Key"
                ),
                SimpleParameter<bool>.of(nameof(OverrideSelectedWithSno), x => OverrideSelectedWithSno = x),
                SimpleParameter<int>.of(nameof(Sno), x => Sno = (uint)x)
            };
        }

        protected override bool Applicable(IController hud, IPlayerSkill skill)
        {
            return hud.Game.Me.Powers.BuffIsActive(OverrideSelectedWithSno ? Sno : SelectedSno);
        }
    }
}