// -----------------------------------------------------------------------------
//
// This file is a modified version of the original file from cactbot.
// The original file can be found at:
// https://github.com/OverlayPlugin/cactbot/blob/main/types/event.d.ts
//
// The original file is licensed under the Apache License, Version 2.0.
//
// -----------------------------------------------------------------------------


export interface EventMap {
  'onPingStatusUpdateEvent': (ev: {
    type: 'onPingStatusUpdateEvent';
    detail?: {
      statusJson?: string;
    };
  }) => void;
  'onPingRemoteAddressChangedEvent': (ev: {
    type: 'onPingRemoteAddressChangedEvent';
    detail?: {
      address: string | null;
    };
  }) => void;
}

export type EventType = keyof EventMap;
