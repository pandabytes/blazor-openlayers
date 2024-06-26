import { MapOptions, default as OpenLayerMap } from 'ol/Map';
import { InvalidArgumentError } from './errors';

class OpenLayersInterop {
  private maps = new Map<string, OpenLayerMap>();

  public createMap(mapId: string, mapOptions: MapOptions): void {
    if (this.mapExist(mapId)) {
      return;
    }

    const map = new OpenLayerMap(mapOptions);
    this.maps.set(mapId, map);
  }

  public getMap(mapId: string): OpenLayerMap {
    const map = this.maps.get(mapId);
    if (!map) {
      throw new InvalidArgumentError(`Map "${mapId}" not found.`);
    }
    return map;
  }

  public mapExist(mapId: string): boolean {
    return this.maps.has(mapId);
  }
}

export const OpenLayersInteropObj = new OpenLayersInterop();
