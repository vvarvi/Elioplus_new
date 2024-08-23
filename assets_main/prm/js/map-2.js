var map = new GMaps({
            el: '#googleMap',
            lat: 23.7386365,
            lng: 90.4068199,

        });

        map.addMarker({
            lat: 23.7386365,
            lng: 90.4068199,
            title: 'Marker with InfoWindow',
            infoWindow: {
                content: '<p>Spellbit Ltd</p>'
            }
        });