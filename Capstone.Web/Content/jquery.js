<script src="https://unpkg.com/masonry-layout@4/dist/masonry.pkgd.min.js"></script>

$('.grid').masonry({
    // set itemSelector so .grid-sizer is not used in layout
    itemSelector: '.grid-item',
    // use element for option
    columnWidth: 'grid-sizer',
    percentPosition: true
})

$(".dropdown").on("show.bs.dropdown", function (event) {
    var x = $(event.relatedTarget).text(); // Get the text of the element
    alert(x);
});