$(() => {

    loadPeople();

    function loadPeople() {
        $.get('/people/getall', function (people) {
            $("#people-table tr:gt(0)").remove();
            people.forEach(person => {
                $("#people-table tbody").append(`
<tr>
    <td>${person.firstName}</td>
    <td>${person.lastName}</td>
    <td>${person.age}</td>
 <td><button class="btn btn-info" id="edit-btn" data-first-name=${person.firstName} data-last-name=${person.lastName}
               data-age=${person.age} data-id=${person.id}>Edit</button></td>
  <td><button class="btn btn-danger" id="delete-btn" data-id=${person.id}>Delete</button></td>

</tr>`);
            });
        });
    }

    $("#add-person").on('click', function () {
        const firstName = $("#first-name").val();
        const lastName = $("#last-name").val();
        const age = $("#age").val();


        $.post('/people/addperson', { firstName, lastName, age }, function (person) {
            //console.log(person.id);
            loadPeople();
            $("#first-name").val('');
            $("#last-name").val('');
            $("#age").val('');
        });
    });

    $("#people-table").on('click', '#delete-btn', function () {
        const id = $(this).data('id');
        console.log(id);

        $.post('/people/delete', { id }, function () {
            loadPeople();
        })
    });

    $("#people-table").on('click', '#edit-btn', function () {

        $('#first-modal').val($(this).data('firstName'));
        $('#last-modal').val($(this).data('lastName'));
        $('#age-modal').val($(this).data('age'));
        $('#id-modal').val($(this).data('id'));

        $('#EditModal').modal();

    });

    $('#EditModal').on('click', '#save-btn', function () {

        const id = $('#id-modal').val();
        const firstName = $('#first-modal').val();
        const lastName = $('#last-modal').val();
        const age = $('#age-modal').val();
        console.log(age);

        $.post('/people/edit', { id, firstName, lastName, age }, function () {
            $('#EditModal').modal('hide');
            loadPeople();
        });
    });

    







});