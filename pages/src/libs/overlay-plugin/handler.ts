// -----------------------------------------------------------------------------
//
// This file is a modified version of the original file from cactbot.
// The original file can be found at:
// https://github.com/OverlayPlugin/cactbot/blob/main/types/event.d.ts
//
// The original file is licensed under the Apache License, Version 2.0.
//
// -----------------------------------------------------------------------------


type BroadcastHandler = (msg: {
  call: 'broadcast';
  source: string;
  msg: unknown;
}) => void;

type SubscribeHandler = (msg: {
  call: 'subscribe';
  events: string[];
}) => void;

type OpenWebsiteWithWSHandler = (msg: {
  call: 'openWebsiteWithWS';
  url: string;
}) => void;

export type OverlayHandlerAll = {
  'broadcast': BroadcastHandler;
  'subscribe': SubscribeHandler;
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
