var urlget = '/Users/' + 'Get';
urlget += "?sortField=" + "FirstName";
urlget += "&sortOrder=" + "ASC";
urlget += "&favColors=" + "All";
urlget += "&favDrinks=" + "All";

$.ajax({
    url: urlget,
    type: 'GET',
    dataType: 'json',
    contentType: "application/json;charset=utf-8",
    success: function (response) {
        var viewModel = new UserIndexViewModel(response);
        ko.applyBindings(viewModel, document.getElementById("userlist"));
    },
    error: function (err) { alert("Error: " + err.responseText); }
});

function UserIndexViewModel(resultList) {
    var self = this;
    self.userService = new UserService(resultList);
};

function UserService(resultList) {

    var self = this;

    self.queryOptions = {
        currentPage: ko.observable(),
        totalPages: ko.observable(),
        pageSize: ko.observable(),
        sortField: ko.observable(),
        sortOrder: ko.observable(),
        favColors: ko.observable(),
        favDrinks: ko.observable()
    };

    self.sending = ko.observable(false);
    self.entities = ko.observableArray();

    self.updateResultList = function (resultList) {
        self.queryOptions.currentPage(resultList.queryOptions.currentPage);
        self.queryOptions.totalPages(resultList.queryOptions.totalPages);
        self.queryOptions.pageSize(resultList.queryOptions.pageSize);
        self.queryOptions.sortField(resultList.queryOptions.sortField);
        self.queryOptions.sortOrder(resultList.queryOptions.sortOrder);

        self.entities(resultList.results);
    };

    self.updateResultList(resultList);

    self.sortEntitiesBy = function (data, event) {

        var sortField = $(event.target).data('sortField');
        console.log(resultList.queryOptions);
        if (sortField == self.queryOptions.sortField() && self.queryOptions.sortOrder() == "ASC")
            self.queryOptions.sortOrder("DESC");
        else
            self.queryOptions.sortOrder("ASC");
        self.queryOptions.sortField(sortField);
        self.queryOptions.currentPage(1);

        self.fetchEntities(event);
    };

    self.favColorsFilter = function (data, event) {

        var colorname = $('#color-filter').val(); 
        var drinkname = $('#drink-filter').val();  
        self.queryOptions.favColors(colorname);
        self.queryOptions.favDrinks(drinkname);

        self.fetchEntitiesColors(event); 
    };
    
    self.favDrinksFilter = function (data, event) {
        
        var colorname = $('#color-filter').val();
        var drinkname = $('#drink-filter').val(); 
        self.queryOptions.favColors(colorname);
        self.queryOptions.favDrinks(drinkname);

        self.fetchEntitiesDrinks(event); 
    };
    
    self.previousPage = function (data, event) {
        if (self.queryOptions.currentPage() > 1) {
            self.queryOptions.currentPage(self.queryOptions.currentPage() - 1);
            self.fetchEntities(event);
        }
    };

    self.nextPage = function (data, event) {
        if (self.queryOptions.currentPage() < self.queryOptions.totalPages()) {
            self.queryOptions.currentPage(self.queryOptions.currentPage() + 1);
            self.fetchEntities(event);
        }
    };

    self.fetchEntities = function (event) {
                
        self.sending(true);        
        
        var urlget = '/Users/' + 'Get';        
        urlget += "?sortField=" + self.queryOptions.sortField();
        urlget += "&sortOrder=" + self.queryOptions.sortOrder();
        urlget += "&currentPage=" + self.queryOptions.currentPage();
        urlget += "&pageSize=" + self.queryOptions.pageSize();
        urlget += "&favColors=" + self.queryOptions.favColors();
        urlget += "&favDrinks=" + self.queryOptions.favDrinks();              
      
        $.ajax({
            url: urlget,
            type: 'GET',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                self.updateResultList(response);
            },
            error: function (err) { alert("Error: " + err.responseText); }
        });                     
    };


    self.fetchEntitiesColors = function (event) {

        var urlget = '/Users/' + 'Get';
        urlget += "?sortField=" + self.queryOptions.sortField();
        urlget += "&sortOrder=" + self.queryOptions.sortOrder();
        urlget += "&currentPage=" + self.queryOptions.currentPage();
        urlget += "&pageSize=" + self.queryOptions.pageSize();
        urlget += "&favColors=" + self.queryOptions.favColors();
        urlget += "&favDrinks=" + self.queryOptions.favDrinks();        

        $.ajax({
            url: urlget,
            type: 'GET',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                self.updateResultList(response);
            },
            error: function (err) { alert("Error: " + err.responseText); }
        });         
    };

    self.fetchEntitiesDrinks = function (event) {

        var urlget = '/Users/' + 'Get';
        urlget += "?sortField=" + self.queryOptions.sortField();
        urlget += "&sortOrder=" + self.queryOptions.sortOrder();
        urlget += "&currentPage=" + self.queryOptions.currentPage();
        urlget += "&pageSize=" + self.queryOptions.pageSize();
        urlget += "&favColors=" + self.queryOptions.favColors();
        urlget += "&favDrinks=" + self.queryOptions.favDrinks();

        $.ajax({
            url: urlget,
            type: 'GET',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                self.updateResultList(response);
            },
            error: function (err) { alert("Error: " + err.responseText); }
        });
    };

};