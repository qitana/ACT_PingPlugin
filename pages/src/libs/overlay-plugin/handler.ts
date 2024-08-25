// -----------------------------------------------------------------------------
//
// This file is a modified version of the original file from cactbot.
// The original file can be found at:
// https://github.com/OverlayPlugin/cactbot/blob/main/types/event.d.ts
//
// The original file is licensed under the Apache License, Version 2.0.
//
// -----------------------------------------------------------------------------


// Member names taken from OverlayPlugin's MiniParse.cs
// Types taken from FFXIV parser plugin
export interface PluginCombatantState {
  CurrentWorldID?: number;
  WorldID?: number;
  WorldName?: string;
  BNpcID?: number;
  BNpcNameID?: number;
  PartyType?: number;
  ID?: number;
  OwnerID?: number;
  WeaponId?: number;
  Type?: number;
  Job?: number;
  Level?: number;
  Name?: string;
  CurrentHP: number;
  MaxHP: number;
  CurrentMP: number;
  MaxMP: number;
  PosX: number;
  PosY: number;
  PosZ: number;
  Heading: number;

  MonsterType?: number;
  Status?: number;
  ModelStatus?: number;
  AggressionStatus?: number;
  TargetID?: number;
  IsTargetable?: boolean;
  Radius?: number;
  Distance?: string;
  EffectiveDistance?: string;
  NPCTargetID?: number;
  CurrentGP?: number;
  MaxGP?: number;
  CurrentCP?: number;
  MaxCP?: number;
  PCTargetID?: number;
  IsCasting1?: number;
  IsCasting2?: number;
  CastBuffID?: number;
  CastTargetID?: number;
  CastGroundTargetX?: number;
  CastGroundTargetY?: number;
  CastGroundTargetZ?: number;
  CastDurationCurrent?: number;
  CastDurationMax?: number;
  TransformationId?: number;
}

type BroadcastHandler = (msg: {
  call: 'broadcast';
  source: string;
  msg: unknown;
}) => void;

type SubscribeHandler = (msg: {
  call: 'subscribe';
  events: string[];
}) => void;

type GetCombatantsHandler = (msg: {
  call: 'getCombatants';
  ids?: number[];
  names?: string[];
  props?: string[];
}) => { combatants: PluginCombatantState[] };

type OpenWebsiteWithWSHandler = (msg: {
  call: 'openWebsiteWithWS';
  url: string;
}) => void;

export type OverlayHandlerAll = {
  'broadcast': BroadcastHandler;
  'subscribe': SubscribeHandler;
  'getCombatants': GetCombatantsHandler;
  'openWebsiteWithWS': OpenWebsiteWithWSHandler;
};

export type OverlayHandlerTypes = keyof OverlayHandlerAll;

export type OverlayHandlerResponseTypes = {
  [call in OverlayHandlerTypes]: ReturnType<OverlayHandlerAll[call]>;
};

export type OverlayHandlerResponses = {
  [call in OverlayHandlerTypes]: Promise<OverlayHandlerResponseTypes[call]>;
};

export type OverlayHandlerFuncs = {
  [call in OverlayHandlerTypes]: (
    msg: Parameters<OverlayHandlerAll[call]>[0],
  ) => OverlayHandlerResponses[call];
};



type UnionToIntersection<U> = (U extends U ? (k: U) => void : never) extends ((k: infer I) => void)
  ? I
  : never;
export type IOverlayHandler = UnionToIntersection<OverlayHandlerFuncs[OverlayHandlerTypes]>;
