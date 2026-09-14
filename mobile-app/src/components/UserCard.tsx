import { StyleSheet, Text, View } from "react-native";
import { User } from "../types/user";

interface UserCardProps {
    user: User;
}

export default function UserCard({ user }: UserCardProps) {
    return (
        <View style={styles.card}>
            <Text style={styles.name}>{user.name}</Text>
            <Text>{user.email}</Text>
            <Text>{user.active ? 'Active' : 'Inactive'}</Text>
        </View>
    );
}

const styles = StyleSheet.create({
    card: {
        padding: 16,
        marginBottom: 10,
        borderWidth: 1,
        borderRadius: 8,
    },

    name: {
        fontSize: 18,
        fontWeight: 'bold',
    },
});