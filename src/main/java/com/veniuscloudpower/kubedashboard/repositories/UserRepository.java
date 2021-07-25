package com.veniuscloudpower.kubedashboard.repositories;

import com.veniuscloudpower.kubedashboard.models.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import java.util.Optional;

public interface UserRepository extends JpaRepository<User, Integer> {

    @Query("SELECT u FROM User u WHERE u.userName like :userName")
    public User getUserByUsername(@Param("userName") String userName);

    public  Optional<User> findByUserName(String userName);

    public Boolean existsByUserName(String userName);

    public Boolean existsByEmail(String email);
}

