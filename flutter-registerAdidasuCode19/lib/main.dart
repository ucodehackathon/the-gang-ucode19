import 'dart:async';

import 'package:flutter/material.dart';
import 'package:register_adidas/screens/screen_landing.dart';

void main() => runApp(MyApp());

class MyApp extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Register Adidas',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: MyHomePage(),
    );
  }
}

class MyHomePage extends StatefulWidget {
  @override
  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  @override
  void initState() {
    super.initState();
    Timer(Duration(seconds: 2), () {
      Navigator.pushReplacement(
          context, MaterialPageRoute(builder: (context) => ScreenLanding()));
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Row(
        children: <Widget>[
          Expanded(
            child: Column(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: <Widget>[
                Flexible(
                  flex: 9,
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: <Widget>[
                      Padding(
                        padding: const EdgeInsets.only(bottom: 24.0),
                        child: Text(
                          "Register Adidas",
                          style: TextStyle(
                            fontSize: 50,
                            fontFamily: 'Adidas Half Block 2016',
                            color: Color.fromARGB(
                                255, 41, 35, 92), // #29235C (uCode color)
                          ),
                        ),
                      ),
                      Image.asset(
                        'assets/ucode.png',
                      ),
                    ],
                  ),
                ),
                Flexible(
                    flex: 1,
                    child: Text(
                      "The Gang (uCode19)",
                      style: TextStyle(color: Colors.grey[300]),
                    ))
              ],
            ),
          ),
        ],
      ),
    );
  }
}
