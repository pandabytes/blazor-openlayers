import { default as OpenLayerMap } from 'ol/Map';
import { InvalidArgumentError } from './errors';
import TileLayer from 'ol/layer/Tile';
import OSM from 'ol/source/OSM';
import { Source } from 'ol/source';
import Layer from 'ol/layer/Layer';
import { View } from 'ol';
import { ViewOptions } from 'ol/View';

const LayerTypes = ['Tile'] as const;
const LayerSources = ['OSM'] as const;

type LayerType = typeof LayerTypes[number];
type LayerSource = typeof LayerSources[number];

/**
 * Mirror C# type.
 */
type MapOptionsWrapper = {
  viewOptions: ViewOptions
  layers: {
    [key in LayerType]: LayerSource;
  }
};

class OpenLayersInterop {
  private maps = new Map<string, OpenLayerMap>();

  public createMap(mapId: string, mapOptions: MapOptionsWrapper): void {
    if (this.mapExist(mapId)) {
      return;
    }

    const map = new OpenLayerMap({ 
      target: mapId,
      view: mapOptions.viewOptions ? new View(mapOptions.viewOptions) : undefined
    });

    if (mapOptions.layers) {
      const layersArray = Object.entries(mapOptions.layers).map(entry => {
        const layerType = entry[0] as LayerType;
        const layerSource = entry[1];
  
        const layer = OpenLayersInterop.getLayer(layerType);
        const source = OpenLayersInterop.getLayerSource(layerSource);
  
        if (!layer || !source) {
          throw new InvalidArgumentError(`Invalid layer type "${layerType}" and/or source "${layerSource}".`);
        }
  
        layer.setSource(source);
        return layer;
      });

      map.setLayers(layersArray);
    }

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

  private static getLayer(layerType: LayerType): Layer | undefined {
    if (layerType === 'Tile') {
      return new TileLayer();
    }
    return undefined;
  }

  private static getLayerSource(layerSource: LayerSource): Source | undefined {
    if (layerSource === 'OSM') {
      return new OSM();
    }
    return undefined;
  }
}

export const OpenLayersInteropObj = new OpenLayersInterop();
