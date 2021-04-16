function renderMap(accessToken, longitude, latitude, zoom, isoCode) {
    mapboxgl.accessToken = accessToken;

    var map = new mapboxgl.Map({
        container: 'map',
        style: 'mapbox://styles/mapbox/streets-v11',
        center: [longitude, latitude],
        zoom: zoom
    });

    map.on('load', function () {
    map.addLayer(
        {
            id: 'country-boundaries',
            source: {
                type: 'vector',
                url: 'mapbox://mapbox.country-boundaries-v1',
            },
            'source-layer': 'country_boundaries',
            type: 'fill',
            paint: {
                'fill-color': '#d2361e',
                'fill-opacity': 0.4,
            },
        },
        'country-label'
    );

    map.setFilter('country-boundaries', [
        "in",
        "iso_3166_1",
        isoCode
        ]);
    });
};