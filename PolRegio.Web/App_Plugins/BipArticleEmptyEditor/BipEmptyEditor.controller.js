angular.module("umbraco")
.controller("Bip.texteditor",
function ($scope) {
    $scope.textInput = {
        label: 'bodyText',
        description: 'Load some stuff here',
        view: 'rte',
        value: "",
        config: {
            editor: {
                toolbar: ["bold"],

                dimensions: { height: 200 }
            }
        }
    };
    $scope.$watch('textInput.value', function (newValue, oldValue) {
        $scope.model.value = newValue;
    });
});