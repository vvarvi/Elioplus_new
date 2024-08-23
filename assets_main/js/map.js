var map;
jQuery(document).ready(function(){

    map = new GMaps({
        div: '#map',
        lat: 37.9706072,
        lng: 23.7405495,
    });
    map.addMarker({
        lat: 37.9706072,
        lng: 23.7405495,
        title: 'Address',      
        infoWindow: {
            content: '<h5 class="title">Elioplus</h5><p><span class="region">33 Saronikou St , 163 45, Ilioupoli, Athens</span><br><span class="postal-code">106 74</span><br><span class="country-name">Greece</span></p>'
        }
        
    });

});