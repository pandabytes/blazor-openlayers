import { default as OpenLayerMap } from 'ol/Map';
import { InvalidArgumentError } from './errors';
import TileLayer from 'ol/layer/Tile';
import OSM from 'ol/source/OSM';
import { Source } from 'ol/source';
import Layer from 'ol/layer/Layer';
import { MapBrowserEvent, Overlay, View } from 'ol';
import { ViewOptions } from 'ol/View';
import { Coordinate, toStringHDMS } from 'ol/coordinate';
import { fromLonLat } from 'ol/proj';
import { Options as OpenLayerOverlayOptions } from 'ol/Overlay';

const LayerTypes = ['Tile'] as const;
const LayerSources = ['OSM'] as const;

type LayerType = typeof LayerTypes[number];
type LayerSource = typeof LayerSources[number];

type OverlayOptions = Omit<OpenLayerOverlayOptions, 'element'> & { elementId?: string; };

/**
 * Mirror C# type.
 */
type MapOptionsWrapper = {
  viewOptions: ViewOptions
  layers: { [key in LayerType]: LayerSource; },
};

// type BaseEventSlim = {
//   mapId: string;
// };

// type MapEventSlim = BaseEventSlim & {
//   type: string;
// };

// type MapBrowserEventSlim = MapEventSlim & {
//   coordinate: Coordinate;
//   dragging: boolean;
//   // frameRate
//   // pixel
//   // target
// };

class OpenLayersInterop {
  private maps = new Map<string, OpenLayerMap>();

  public createMap(mapId: string, mapOptions: MapOptionsWrapper, overlays?: { [elementId: string]: OverlayOptions; }): void {
    if (this.mapExist(mapId)) {
      return;
    }

    console.log(mapOptions);
    console.log(overlays);

    const map = new OpenLayerMap({ 
      target: mapId,
      view: mapOptions.viewOptions ? new View(mapOptions.viewOptions) : undefined,
      overlays: OpenLayersInterop.getOverlays(overlays),
      layers: OpenLayersInterop.getLayers(mapOptions.layers),
    });

    // map.on('singleclick', function (evt) {
    //   const coordinate = evt.coordinate;
    
    //   const overlays: Array<Overlay> = [];
    //   map.getOverlays().forEach(o => overlays.push(o));

    //   // Object.keys(overlayHandlers).forEach(id => {
    //   //   const overlay = overlays.filter(o => o.getElement().id === id)[0];  
    //   //   console.log(`id: ${id} - ${overlay}`)
    //   //   overlay.setPosition(coordinate);

    //   //   const element = overlay.getElement();
    //   //   const handler = overlayHandlers[id];
    //   //   element.innerHTML = handler(coordinate);
    //   // });

    //   // const overlay = overlays.filter(o => o.getElement().id === 'map-test-p3')[0];
    //   // const element = overlay.getElement();

    //   // overlay.setPosition(coordinate);
    //   // element.innerHTML = `<p>You clicked here:</p><code>${coordinate}</code>`;

    //   // const e = overlays.filter(o => o.g)[0].getElement();
    //   // e.innerHTML = '<p>hello</p>';
    //   // overlays[0].setPosition(coordinate);
    // });

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

  public removeMap(mapId: string) {
    if (!this.mapExist(mapId)) {
      return;
    }

    // https://github.com/openlayers/openlayers/issues/11052
    // https://github.com/openlayers/openlayers/pull/3420
    const openLayerMap = this.getMap(mapId);
    openLayerMap.setTarget(null);
    this.maps.delete(mapId);
  }

  // public registerSingleClickHandler(mapId: string, eventHandler: (args: any) => void) {
  //   const openLayerMap = this.getMap(mapId);

  //   openLayerMap.on('singleclick', event => {
  //     eventHandler({
  //       mapId: mapId,
  //       type: event.type,
  //       coordinate: event.coordinate,
  //       dragging: event.dragging
  //     });
  //   });
  // }

  private static getOverlays(inputOverlays?: { [elementId: string]: OverlayOptions; }): Array<Overlay> {
    if (!inputOverlays) {
      return [];
    }

    return Object.keys(inputOverlays).map(elementId => {
      const overlayOpts = inputOverlays[elementId];
      const position = (overlayOpts.position) ? fromLonLat(overlayOpts.position) : undefined;
      
      return new Overlay({
        ...overlayOpts,
        position,
        element: document.getElementById(elementId),
      });
    });
  }

  private static getLayers(inputLayers?: { [key in LayerType]: LayerSource; }): Array<Layer> {
    if (!inputLayers) {
      return [];
    }

    return Object.entries(inputLayers).map(entry => {
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
