# API REST Alergenos
Creacion de API REST que devuelve los alérgenos de un plato dado, o los platos en los que aparece un alérgeno concreto, y permite añadir ingredientes, platos y alérgenos.
# Diseño BBDD
El diseño de la base de datos consta de 3 tablas principales tbPlatos, tbIngredientes y tbAlergenos y de 2 tablas mas tbIng_Plato y tbAlerg_Ing que sirven para relaccionar las otras 3 entre si, teniendo los ids de las tablas principales
# Naming
Campos BBDD:
Cada palabra con la primera letra en mayusculas y separadas por "_"

Ejemplo: Id_Plato

Nombres programa:
Cada palabra con la primera letra en mayuscula y sin separción entre palabras

Ademas dependiento del tipo de variable llevará una letra en minuscula al principio:

pNombre => parametros
vNombre => variables locales
gNombre => variables globales
lNombre => Listas
aNombre => Arrays

En el caso de los DbSet se han llamado con las letras tb. Ejemplo: tbPlatos

Ejemplo: vIdPlato

# Explicacion Decisiones
No se ha realizado separacion en capas puesto que se considera que una API ya es una capa de un sistema mayor.

Se han utilizado Collection para las recursividad en los clases que hacen referencia a los registros de las tablas por que son mas faciles de manejar en codigo

Se ha decido que al dar de alta un plato se de alta tambien de alta en la tbIng_Plato su relacción con los ingredientes ya que parece mas natural que hacerlo a la inversa y realizarlo de manera por separado puedo provocar errores en el usuario de la API que se le olvide realizar el alta de la tabla intermedia.

Se ha realizado lo mismo con los Ingredientes y su relacción con los Alergenos por el mismmo motivo

No se han modificado las páginas de ayuda de la API que visual Studio realiza automáticamente por entender que escapa a los requisitos de la prueba y la información que da es suficiente para saber como funciona la API

Se ha creado un Controller por cada acción aunque se valoro la posibilidad de poner todos los POST en el mismo controller sobrecargando la funcion pero se ha dejado un controller para cada post por si hubiera que añadir otra funcion en el futuro (por ejemplo GET)

# Pruebas

Se han realizado pruebas de todas las funciones por separado utilizando la extension de chrome "Cliente de Servicios Web RESTful"

# BONUS: Diseñar un LOG para que guarde los cambios de los ingredientes en los platos
 Se haría de tal manera que el cambio llegaria como un objeto de clase tbPlatos con la información del plato y los cambios de ingredientes estarián en la Colección tbIng_Plato
 
 Se buscaria por el id del plato el plato guardado previamente en la BBDD y se recorria su coleccion tbIng_Plato y se buscaria el correspondiente en el nuevo objetos, asi, si no cambia ni el id del plato ni el del ingrediente se marcaria como UPDATE indicando el valor del antiguo y del nuevo si existiera en el nuevo pero no en el de la BBDD se mmarcaria coomo INSERT, y si fuera a la inversa se marcaria como DELETE. Si fuera igual en ambos objetos se ignoraria puesto que no se ha realizado cambios en ese ingrediente
 
 Se crearia un String para cada accion del tipo UPDATE {ingrediente} valor antiguo = {valorantiguo} valor nuevo = {valornuevo} y tras comprobar que el objeto nuevo actualiza en la BBDD se guardaria el Log añadiendole la fecha y la hora actual
 
 Para guardar el Log dependiendo del uso que se le fuera a dar al LOG se guardaria en un BBDD si es para un uso más intensivo o en un fichero de texto plato (por ejemplo .txt) si fuera para un uso menos intensivo
 
 En caso de guardarse en BBDD se crearian 2 tablas relaccionadas una que seria la cabezara del LOG donde se guardia fecha, hora y que plato se ha modificado y otra que seria los detalles donde se guardaria el ingmodificado, el tipo de modificacion y valores antiguos y nuevos para el caso de los UPDATE y que en el resto de casos quedarian nulos
