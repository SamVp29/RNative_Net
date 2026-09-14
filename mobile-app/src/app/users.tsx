import UserCard from "@/components/UserCard";
import { userService } from "@/services/userService";
import { FlatList, StyleSheet, Text, View } from "react-native";

export default function Users() {
    const users = userService.getUsers();
    return (
        <View style={styles.container}>
            <Text style={styles.title}>Usuarios</Text>
            <FlatList data={users} keyExtractor={(item) => item.id.toString()} renderItem={({ item }) => (
                <UserCard user={item} />
            )} />
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
    },
});