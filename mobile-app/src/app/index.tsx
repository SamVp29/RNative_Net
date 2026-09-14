import { Link } from "expo-router";
import { StyleSheet, Text, View } from "react-native";

export default function Index(){
  return (
    <View style={styles.container}>
      <Text style={styles.title}>
        Gestión de Usuarios
      </Text>
      <Text>React Native + Expo</Text>
      <Link href="/users" style={{ marginTop: 20, color: 'blue' }}>
        Ver usuarios
      </Link>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },

  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 10,
  }
})
