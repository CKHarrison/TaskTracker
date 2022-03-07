// Get cards
function resizeCards(cards) {
    var maxHeight = 200;

    // Loop all cards and check height, if bigger than max then save it
    for (var i = 0; i < cards.length; i++) {
        if (maxHeight < cards[i].outerHeight()) {
            maxHeight = cards[i].outerHeight();
        }
    }
    // Set ALL card bodies to this height
    for (var i = 0; i < cards.length; i++) {
        cards[i].height(maxHeight);
    }
}
resizeCards(document.querySelectorAll('card'));