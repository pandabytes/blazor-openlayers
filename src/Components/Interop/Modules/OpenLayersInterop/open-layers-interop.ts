import { MapOptions, default as OpenLayerMap } from 'ol/Map';
import { InvalidArgumentError } from './errors';
import TileLayer from 'ol/layer/Tile';
import OSM from 'ol/source/OSM';
import { Source } from 'ol/source';
import Layer from 'ol/layer/Layer';
import { View } from 'ol';

import { ViewOptions } from 'ol/View';

type MapOptionsWrapper = {
  target: string;
  viewOptions: ViewOptions
};

class OpenLayersInterop {
  private maps = new Map<string, OpenLayerMap>();

  public createMap(mapId: string, mapOptions: MapOptionsWrapper): void {
    if (this.mapExist(mapId)) {
      return;
    }

    // const map = new OpenLayerMap();
    const map = new OpenLayerMap({ 
      target: mapId,
      view: new View(mapOptions.viewOptions) 
    });

    // map.addLayer(new TileLayer({source: new OSM()}));
    // map.setLayerGroup(group);

    // const map = new OpenLayerMap({
    //   layers: [
    //     new TileLayer({source: new OSM()}),
    //   ],
    //   view: new View({
    //     center: [0, 0],
    //     zoom: 2,
    //   }),
    //   target: mapId,
    // });

    this.maps.set(mapId, map);
  }

  public setLayers(mapId: string, layers: { [key: string]: string }) {
    const t = new TileLayer({source: new OSM()});
    // const x = tileLayers[1]
    const map = this.getMap(mapId);

    const layersArray = Object.entries(layers).map(entry => {
      const layerType = entry[0];
      const layerSource = entry[1];

      const layer = OpenLayersInterop.getLayer(layerType);
      const source = OpenLayersInterop.getLayerSource(layerSource);

      if (!layer || !source) {
        throw new InvalidArgumentError(`Invalid layer type ${layerType} and source ${layerSource}.`);
      }

      layer.setSource(source);
      source.refresh();
      return layer;
    })

    // map.addLayer(t);
    
    // const group = new Group({ layers: layersArray });
    // const layerCollection = new Collection([t]);
    // group.setLayers(layerCollection);
    // map.setLayerGroup(group);

    // var layersOSM = new Group({
    //   layers: [
    //       new TileLayer({
    //           source: new OSM()
    //       })
    //   ]
    // });
    // map.setLayerGroup(layersOSM);

    map.setLayers(layersArray);
    // map.renderSync();
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

  private static getLayer(layerType: string): Layer | undefined {
    if (layerType === 'Tile') {
      return new TileLayer();
    }

    return undefined;
  }

  private static getLayerSource(layerSource: string): Source | undefined {
    if (layerSource === 'OSM') {
      return new OSM();
    }

    return undefined;
  }
}

export const OpenLayersInteropObj = new OpenLayersInterop();
