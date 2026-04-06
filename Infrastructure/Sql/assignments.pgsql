CREATE TABLE assignments (
    id UUID PRIMARY KEY,
    title VARCHAR(200) NOT NULL,
    description TEXT NOT NULL,
    status SMALLINT NOT NULL DEFAULT 1, -- Asumiendo que 0 es 'Pending' en tu Enum
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITHOUT TIME ZONE DEFAULT NULL,
    
    -- El que asigna la tarea
    assigned_by_id UUID NOT NULL,
    
    -- El que debe realizarla
    assigned_to_id UUID NOT NULL,

    -- Definición de las relaciones (Foreign Keys)
    CONSTRAINT fk_user_assigned_by 
        FOREIGN KEY (assigned_by_id) 
        REFERENCES users(id) 
        ON DELETE RESTRICT, -- No permite borrar al usuario si tiene tareas asignadas
        
    CONSTRAINT fk_user_assigned_to 
        FOREIGN KEY (assigned_to_id) 
        REFERENCES users(id) 
        ON DELETE RESTRICT
);

-- Índices para mejorar la velocidad de búsqueda por usuario
CREATE INDEX idx_assignments_assigned_by ON assignments(assigned_by_id);
CREATE INDEX idx_assignments_assigned_to ON assignments(assigned_to_id);