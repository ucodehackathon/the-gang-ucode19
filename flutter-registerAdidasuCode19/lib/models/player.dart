class Player {
  int height;
  int weight;
  int age;
  String mainFoot;
  int shoeSize;
  int number;

  Player(this.height, this.weight, this.age, this.mainFoot, this.shoeSize,
      this.number);

  map() => {
        "height": height,
        "weight": weight,
        "age": age,
        "mainFoot": mainFoot,
        "shoeSize": shoeSize,
        "number": number,
      };

  Player.fromMap(Map map)
      : height = map['height'],
        weight = map['weight'],
        age = map['age'],
        mainFoot = map['mainFoot'],
        shoeSize = map['shoeSize'],
        number = map['number'];

  @override
  String toString() {
    return 'Player{height: $height, weight: $weight, age: $age, mainFoot: $mainFoot, shoeSize: $shoeSize, number: $number}';
  }

  Player.empty();
}
